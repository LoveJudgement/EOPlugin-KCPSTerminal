using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Nancy;

namespace KCPSTerminal
{
	public class KCPSTerminalNancyModule : NancyModule
	{
		private static readonly ImageCodecInfo JpgEncoder =
			ImageCodecInfo.GetImageDecoders().First(i => i.FormatID == ImageFormat.Jpeg.Guid);

		public KCPSTerminalNancyModule()
		{
			Get("/capture", _ =>
			{
				var bitmap = BrowserOperator.Singleton.CaptureScreen();
				var stream = new MemoryStream();

				var compressionLevel = Plugin.Singleton.Settings.JpegCompressionLevel;
				if (compressionLevel == 0)
				{
					bitmap.Save(stream, ImageFormat.Png);
				}
				else
				{
					bitmap.Save(stream, JpgEncoder, new EncoderParameters
					{
						Param = new[] {new EncoderParameter(Encoder.Quality, compressionLevel)}
					});
				}

				stream.Seek(0, SeekOrigin.Begin);
				return Response.FromStream(stream, compressionLevel == 0 ? "image/png" : "image/jpeg");
			});

			Get("/mouse", _ =>
			{
				var x = double.Parse(Request.Query["x"]);
				var y = double.Parse(Request.Query["y"]);
				var type = Request.Query["type"];
				BrowserOperator.Singleton.SendMouseEvent(x, y, type);
				return "";
			});

			Get("/refresh", _ =>
			{
				BrowserOperator.Singleton.Refresh();
				return "";
			});

			Get("/data", _ =>
			{
				var type = Request.Query["type"];

				try
				{
					var response = (Response) DatabaseOperator.Singleton.HandleData(type);
					response.ContentType = "application/json";
					return response;
				}
				catch (NotImplementedException)
				{
					return HttpStatusCode.NotFound;
				}
			});

			Get("/response", _ =>
			{
				var type = Request.Query["type"];

				try
				{
					var response = (Response) DatabaseOperator.Singleton.HandleResponse(type);
					response.ContentType = "application/json";
					return response;
				}
				catch (KeyNotFoundException)
				{
					return HttpStatusCode.NotFound;
				}
			});
		}
	}
}
