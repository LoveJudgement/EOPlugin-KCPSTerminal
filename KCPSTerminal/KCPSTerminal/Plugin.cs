using System;
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

		internal static Plugin Singleton;

		internal Settings Settings;
		private NancyHost _nancyHost;
		private const string SETTINGS_PATH = @"Settings\KCPSTerminal.json";

		internal FormMain FormMain;

		public override bool RunService(FormMain main)
		{
			Singleton = this;

			this.FormMain = main;
			this.Settings = new Settings(); // Temporarily hardcoded config.

			Initialize();
			return true;
		}

		private void Initialize()
		{
			DatabaseOperator.Singleton.StartObserver();

			StartServer();
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
	}
}
