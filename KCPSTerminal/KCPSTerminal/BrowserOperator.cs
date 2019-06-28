﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ElectronicObserver.Utility;

namespace KCPSTerminal
{
	internal class BrowserOperator
	{
		public static BrowserOperator Singleton = new BrowserOperator();

		private BrowserOperator()
		{
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

		public IntPtr FindTargetHandle(IntPtr mainHandle)
		{
			List<IntPtr> childHandles = new List<IntPtr>();

			GCHandle gcChildhandlesList = GCHandle.Alloc(childHandles);
			IntPtr pointerChildHandlesList = GCHandle.ToIntPtr(gcChildhandlesList);

			try
			{
				EnumWindowsProc childProc = EnumWindow;
				EnumChildWindows(mainHandle, childProc, pointerChildHandlesList);
			}
			finally
			{
				gcChildhandlesList.Free();
			}

			foreach (IntPtr handle in childHandles)
			{
				if (IsBrowserWindow(handle))
				{
					return handle;
				}
			}

			foreach (IntPtr handle in childHandles)
			{
				IntPtr possibleHandle = FindTargetHandle(handle);
				if (!possibleHandle.Equals(IntPtr.Zero))
				{
					return possibleHandle;
				}
			}

			return IntPtr.Zero;
		}

		private bool EnumWindow(IntPtr hWnd, IntPtr lParam)
		{
			GCHandle gcChildhandlesList = GCHandle.FromIntPtr(lParam);

			if (gcChildhandlesList == null || gcChildhandlesList.Target == null)
			{
				return false;
			}

			List<IntPtr> childHandles = gcChildhandlesList.Target as List<IntPtr>;
			childHandles.Add(hWnd);

			return true;
		}

		private IntPtr? _cachedHandle;

		private IntPtr GetHandle()
		{
			if (_cachedHandle == null)
			{
				Process process = Process.GetCurrentProcess();
				_cachedHandle = FindTargetHandle(process.MainWindowHandle);
			}

			return _cachedHandle.Value;
		}


		[DllImport("user32.dll")]
		private static extern IntPtr GetWindowRect(IntPtr hWnd, out WinAPI.RECT rect);

		[DllImport("user32.dll")]
		private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

		public Bitmap CaptureScreen()
		{
			var handle = GetHandle();

			WinAPI.RECT rc;
			GetWindowRect(handle, out rc);

			Bitmap bmp = new Bitmap(rc.right - rc.left, rc.bottom - rc.top, PixelFormat.Format32bppArgb);
			Graphics gfxBmp = Graphics.FromImage(bmp);
			IntPtr hdcBitmap = gfxBmp.GetHdc();

			PrintWindow(handle, hdcBitmap, 0x00000002); // PW_RENDERFULLCONTENT

			gfxBmp.ReleaseHdc(hdcBitmap);
			gfxBmp.Dispose();

			return bmp;
		} 

		[DllImport("user32.dll")]
		private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		private static readonly Dictionary<string, int> SupportedMouseEventTypes = new Dictionary<string, int>
		{
			{"down", 0x201}, // WM_LBUTTONDOWN
			{"up", 0x202}, // WM_LBUTTONUP
			{"move", 0x200}, // WM_MOUSEMOVE
		};

		public void SendMouseEvent(double x, double y, string type)
		{
			if (!SupportedMouseEventTypes.ContainsKey(type))
			{
				return;
			}

			var hWnd = GetHandle();

			WinAPI.RECT rc;
			GetWindowRect(hWnd, out rc);

			var xCoordinate = (int) (x * (rc.right - rc.left));
			var yCoordinate = (int) (y * (rc.bottom - rc.top));

			var coordinate = (yCoordinate << 16) | xCoordinate;
			SendMessage(hWnd, SupportedMouseEventTypes[type], 1, coordinate);
		}
	}
}