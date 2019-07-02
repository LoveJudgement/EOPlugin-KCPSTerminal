using System;
using System.IO;
using Codeplex.Data;
using ElectronicObserver.Utility;
using ElectronicObserver.Window;
using ElectronicObserver.Window.Plugins;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Hosting.Self;
using Nancy.TinyIoc;

namespace KCPSTerminal
{
	public class Plugin : ServerPlugin
	{
		public override string MenuTitle => "KCPSTerminal";
		public override string Version => "<BUILD_VERSION>";

		private const string SETTINGS_PATH = @"Settings\KCPSTerminal.json";

		internal static Plugin Singleton;

		internal Settings Settings;
		private NancyHost _nancyHost;

		internal FormMain FormMain;

		public override bool RunService(FormMain main)
		{
			Singleton = this;

			this.FormMain = main;

			Settings = File.Exists(SETTINGS_PATH)
				? (Settings) DynamicJson.Parse(File.ReadAllText(SETTINGS_PATH)).Deserialize<Settings>()
				: new Settings();

			DatabaseOperator.Singleton.StartObserver();

			StartServer();

			return true;
		}

		private void StartServer()
		{
			var address = $"http://localhost:{Settings.Port}";

			_nancyHost?.Stop();
			_nancyHost = new NancyHost(new Bootstrapper(Settings), new HostConfiguration {RewriteLocalhost = false},
				new Uri(address));
			_nancyHost.Start();

			Logger.Add(3, $"KCPSTerminal: Started listening at {address}.");
		}

		private class Bootstrapper : DefaultNancyBootstrapper
		{
			private readonly Settings _settings;

			public Bootstrapper(Settings settings)
			{
				_settings = settings;
			}

			public override void Configure(INancyEnvironment environment)
			{
				environment.Tracing(true, true);
				base.Configure(environment);
			}

			protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
			{
				pipelines.BeforeRequest += (ctx) =>
				{
					Logger.Add(_settings.LogPriority, $"Received request to {ctx.Request.Url}");
					return null;
				};
				base.ApplicationStartup(container, pipelines);
			}
		}

		internal void SaveSettings()
		{
			if (Settings == null)
			{
				return;
			}

			if (!Directory.Exists("Settings"))
			{
				Directory.CreateDirectory("Settings");
			}

			File.WriteAllText(SETTINGS_PATH, DynamicJson.Serialize(Settings));

			StartServer();
		}

		public override PluginSettingControl GetSettings()
		{
			return new SettingsControl();
		}
	}
}
