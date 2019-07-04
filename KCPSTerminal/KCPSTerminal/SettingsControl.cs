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
			textBoxToken.Text = settings.Token;
			numericUpDownCompressionLevel.Value = settings.JpegCompressionLevel;
			numericUpDownLogPriority.Value = settings.LogPriority;
			checkBoxLogResponse.Checked = settings.LogResponse;
		}

		public override bool Save()
		{
			var settings = Plugin.Singleton.Settings;
			settings.Port = (ushort) numericUpDownPort.Value;
			settings.Token = textBoxToken.Text;
			settings.JpegCompressionLevel = (int) numericUpDownCompressionLevel.Value;
			settings.LogPriority = (int) numericUpDownLogPriority.Value;
			settings.LogResponse = checkBoxLogResponse.Checked;

			Plugin.Singleton.SaveSettings();
			return true;
		}
	}
}
