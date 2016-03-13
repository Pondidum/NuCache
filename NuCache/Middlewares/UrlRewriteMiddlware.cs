using System;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.Owin;
using Serilog;

namespace NuCache.Middlewares
{
	public class UrlRewriteMiddlware : OwinMiddleware
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<UrlRewriteMiddlware>();

		private readonly Configuration _config;

		public UrlRewriteMiddlware(OwinMiddleware next, Configuration config) : base(next)
		{
			_config = config;
		}

		public override Task Invoke(IOwinContext context)
		{
			var requestPath = context.Request.Path.ToString();

			Log.Debug("{path}", requestPath);

			if (requestPath.StartsWith("/v3", StringComparison.OrdinalIgnoreCase) == false)
			{
				return Next.Invoke(context);
			}

			var client = new HttpClient()
			{
				BaseAddress = _config.SourceNugetFeed
			};

			var response = client.GetAsync(requestPath).Result;

			context.Response.ContentType = response.Content.Headers.ContentType.MediaType;

			using (var sr = new StreamReader(response.Content.ReadAsStreamAsync().Result))
			using (var sw = new StreamWriter(context.Response.Body))
			{
				var self = context.Request.Uri;
				var replacement = new UriBuilder(self.Scheme, self.Host, self.Port).ToString();
				var source = _config.SourceNugetFeed.ToString();

				string line;
				while ((line = sr.ReadLine()) != null)
				{
					sw.WriteLine(line.Replace(source, replacement));
				}
			}

			return Task.Delay(0);
		}
	}
}
