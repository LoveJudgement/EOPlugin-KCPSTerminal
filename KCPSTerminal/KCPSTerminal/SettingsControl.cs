using System;
using ElectronicObserver.Window.Plugins;

namespace KCPSTerminal
{
	public partial class SettingsControl : PluginSettingControl
	{
		public SettingsControl()
		{
			InitializeComponent();
		}

		private void SettingsControl_Load(object sender, EventArgs e)
		{
			numericUpDownPort.Value = Plugin.Singleton.Settings.Port;
			numericUpDownLogPriority.Value = Plugin.Singleton.Settings.LogPriority;
		}

		public override bool Save()
		{
			Plugin.Singleton.Settings.Port = (ushort) numericUpDownPort.Value;
			Plugin.Singleton.Settings.LogPriority = (int) numericUpDownLogPriority.Value;

			Plugin.Singleton.SaveSettings();
			return true;
		}
	}
}
