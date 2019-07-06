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

			comboBoxMouseEventMode.DataSource = Enum.GetValues(typeof(Settings.MouseEventModeEnum));
			comboBoxMouseEventMode.SelectedItem = settings.MouseEventMode;
		}

		public override bool Save()
		{
			var settings = Plugin.Singleton.Settings;
			settings.Port = (ushort) numericUpDownPort.Value;
			settings.Token = textBoxToken.Text;
			settings.JpegCompressionLevel = (int) numericUpDownCompressionLevel.Value;
			settings.LogPriority = (int) numericUpDownLogPriority.Value;
			settings.LogResponse = checkBoxLogResponse.Checked;
			settings.MouseEventMode = (Settings.MouseEventModeEnum) Enum.Parse(typeof(Settings.MouseEventModeEnum),
				comboBoxMouseEventMode.SelectedItem.ToString());

			Plugin.Singleton.SaveSettings();
			return true;
		}
	}
}
