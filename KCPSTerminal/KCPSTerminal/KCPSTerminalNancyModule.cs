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

		private static readonly EncoderParameters JpgEncoderParameters = new EncoderParameters
		{
			Param = new[] {new EncoderParameter(Encoder.Quality, 80L)}
		};

		public KCPSTerminalNancyModule()
		{
			Get("/capture", _ =>
			{
				var bitmap = BrowserOperator.Singleton.CaptureScreen();
				var stream = new MemoryStream();
				bitmap.Save(stream, JpgEncoder, JpgEncoderParameters);
				stream.Seek(0, SeekOrigin.Begin);
				return Response.FromStream(stream, "image/jpeg");
			});

			Get("/mouse", _ =>
			{
				var x = double.Parse(Request.Query["x"]);
				var y = double.Parse(Request.Query["y"]);
				var type = Request.Query["type"];
				BrowserOperator.Singleton.SendMouseEvent(x, y, type);
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
