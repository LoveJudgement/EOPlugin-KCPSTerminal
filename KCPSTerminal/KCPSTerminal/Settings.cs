using System;

namespace KCPSTerminal
{
	public class Settings
	{
		public ushort Port { get; set; } = 5277;
		public int LogPriority { get; set; } = 1;
		public string Token { get; set; } = "";
		public bool LogResponse { get; set; } = false;
		public int JpegCompressionLevel { get; set; } = 80;
		public bool SkipCopyRawData { get; set; } = false;


		// String version of MouseEventMode. Using string so we don't break DynamicJson.
		// DO NOT use this directly.
		public string _mouseEventMode { get; set; } = MouseEventModeEnum.WinAPI.ToString();

		public enum MouseEventModeEnum
		{
			WinAPI = 0,
			IPC = 1,
		}

		// internal so DynamicJson does not attempt to serialize this.
		internal MouseEventModeEnum MouseEventMode
		{
			get => (MouseEventModeEnum) Enum.Parse(typeof(MouseEventModeEnum), _mouseEventMode);
			set => _mouseEventMode = value.ToString();
		}


		// String version of CaptureMode. Using string so we don't break DynamicJson.
		// DO NOT use this directly.
		public string _captureMode { get; set; } = CaptureModeEnum.WinAPI.ToString();

		public enum CaptureModeEnum
		{
			WinAPI = 0,
			IPC = 1,
		}

		// internal so DynamicJson does not attempt to serialize this.
		internal CaptureModeEnum CaptureMode
		{
			get => (CaptureModeEnum) Enum.Parse(typeof(CaptureModeEnum), _captureMode);
			set => _captureMode = value.ToString();
		}
	}
}
