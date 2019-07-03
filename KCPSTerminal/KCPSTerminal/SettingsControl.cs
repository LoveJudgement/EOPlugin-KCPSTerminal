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
			var settings = Plugin.Singleton.Settings;

			numericUpDownPort.Value = settings.Port;
			numericUpDownLogPriority.Value = settings.LogPriority;
			textBoxToken.Text = settings.Token;
		}

		public override bool Save()
		{
			var settings = Plugin.Singleton.Settings;
			settings.Port = (ushort) numericUpDownPort.Value;
			settings.LogPriority = (int) numericUpDownLogPriority.Value;
			settings.Token = textBoxToken.Text;

			Plugin.Singleton.SaveSettings();
			return true;
		}
	}
}
