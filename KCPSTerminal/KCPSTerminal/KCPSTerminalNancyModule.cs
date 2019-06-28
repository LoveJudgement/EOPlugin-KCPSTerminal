using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace KCPSTerminal
{
	public class KCPSTerminalNancyModule : NancyModule
	{
		private static readonly ImageCodecInfo JpgEncoder =
			ImageCodecInfo.GetImageDecoders().First(i => i.FormatID == ImageFormat.Jpeg.Guid);

		private static readonly EncoderParameters JpgEncoderParameters = new EncoderParameters()
		{
			Param = new[] {new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L)}
		};

		public KCPSTerminalNancyModule(Plugin plugin)
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
				var x = double.Parse(this.Request.Query["x"]);
				var y = double.Parse(this.Request.Query["y"]);
				var type = this.Request.Query["type"];
				BrowserOperator.Singleton.SendMouseEvent(x, y, type);
				return "";
			});

			Get("/data", _ =>
			{
				var type = this.Request.Query["type"];

				try
				{
					var response = (Response) DatabaseOperator.Singleton.HandleData(type);
					response.ContentType = "application/json";
					return response;
				}
				catch (NotImplementedException ex)
				{
					return HttpStatusCode.NotFound;
				}
			});

			Get("/response", _ =>
			{
				var type = this.Request.Query["type"];
				var response = (Response) DatabaseOperator.Singleton.HandleResponse(type);
				response.ContentType = "application/json";
				return response;
			});
		}
	}
}