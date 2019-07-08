using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ElectronicObserver.Utility;

namespace KCPSTerminal
{
	internal class BrowserOperator
	{
		internal static readonly BrowserOperator Singleton = new BrowserOperator();

		private readonly Lazy<IntPtr> _browserHandle;

		private BrowserOperator()
		{
			_browserHandle = new Lazy<IntPtr>(() => FindTargetHandle(Plugin.Singleton.FormMain.fBrowser.HWND));
		}

		private const string BROWSER_CLASS_NAME = "Chrome_RenderWidgetHostHWND";

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

		private static bool IsBrowserWindow(IntPtr hWnd)
		{
			var className = new StringBuilder(256);
			var nRet = GetClassName(hWnd, className, className.Capacity);
			return (nRet != 0) &&
			       (string.Compare(className.ToString(), BROWSER_CLASS_NAME, StringComparison.OrdinalIgnoreCase) == 0);
		}

		private IntPtr FindTargetHandle(IntPtr mainHandle)
		{
			var targetHandles = new List<IntPtr>();

			var gcTargetHandles = GCHandle.Alloc(targetHandles);
			try
			{
				EnumChildWindows(mainHandle, EnumWindow, GCHandle.ToIntPtr(gcTargetHandles));
			}
			finally
			{
				gcTargetHandles.Free();
			}

			return targetHandles.First();
		}

		private bool EnumWindow(IntPtr hWnd, IntPtr lParam)
		{
			if (!IsBrowserWindow(hWnd))
			{
				return true;
			}

			var gcTargetHandles = GCHandle.FromIntPtr(lParam);
			((List<IntPtr>) gcTargetHandles.Target).Add(hWnd);
			return false;
		}

		internal Bitmap CaptureScreen()
		{
			switch (Plugin.Singleton.Settings.CaptureMode)
			{
				case Settings.CaptureModeEnum.WinAPI:
					return CaptureScreenWinApi();

				case Settings.CaptureModeEnum.IPC:
					using (var stream = new MemoryStream(Plugin.Singleton.FormMain.fBrowser.TakeScreenShotAsPngBytes()))
					{
						return (Bitmap) Image.FromStream(stream);
					}

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		[DllImport("user32.dll")]
		private static extern IntPtr GetWindowRect(IntPtr hWnd, out WinAPI.RECT rect);

		[DllImport("user32.dll")]
		private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

		private Bitmap CaptureScreenWinApi()
		{
			GetWindowRect(_browserHandle.Value, out var rc);

			Bitmap bmp = new Bitmap(rc.right - rc.left, rc.bottom - rc.top, PixelFormat.Format32bppArgb);
			Graphics gfxBmp = Graphics.FromImage(bmp);
			IntPtr hdcBitmap = gfxBmp.GetHdc();

			PrintWindow(_browserHandle.Value, hdcBitmap, 0x00000002); // PW_RENDERFULLCONTENT

			gfxBmp.ReleaseHdc(hdcBitmap);
			gfxBmp.Dispose();

			return bmp;
		}


		internal void SendMouseEvent(double x, double y, string type)
		{
			switch (Plugin.Singleton.Settings.MouseEventMode)
			{
				case Settings.MouseEventModeEnum.WinAPI:
					SendMouseEventWinApi(x, y, type);
					break;
				case Settings.MouseEventModeEnum.IPC:
					Plugin.Singleton.FormMain.fBrowser.SendMouseEvent(type, x, y);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		[DllImport("user32.dll")]
		private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		private bool _isLButtonDown = false;

		private static readonly Dictionary<string, int> MouseEventMsgs = new Dictionary<string, int>
		{
			{"down", 0x201}, // WM_LBUTTONDOWN
			{"up", 0x202}, // WM_LBUTTONUP
			{"move", 0x200}, // WM_MOUSEMOVE
		};

		private void SendMouseEventWinApi(double x, double y, string type)
		{
			if (!MouseEventMsgs.ContainsKey(type))
			{
				return;
			}

			GetWindowRect(_browserHandle.Value, out var rc);
			var xCoordinate = (int) (x * (rc.right - rc.left));
			var yCoordinate = (int) (y * (rc.bottom - rc.top));
			var coordinate = (yCoordinate << 16) | xCoordinate;

			if (type == "down") _isLButtonDown = true;
			else if (type == "up") _isLButtonDown = false;

			SendMessage(_browserHandle.Value, MouseEventMsgs[type], _isLButtonDown ? 1 : 0, coordinate);
		}

		internal void Refresh()
		{
			Plugin.Singleton.FormMain.fBrowser.RefreshBrowser();
		}
	}
}
