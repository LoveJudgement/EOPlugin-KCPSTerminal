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

		// String version for MouseEventMode. Using string so we don't break DynamicJson.
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
	}
}
