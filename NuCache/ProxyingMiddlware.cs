using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin;
using Serilog;

namespace NuCache
{
	public class ProxyingMiddlware : OwinMiddleware
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<ProxyingMiddlware>();

		public ProxyingMiddlware(OwinMiddleware next) : base(next)
		{
		}

		public override Task Invoke(IOwinContext context)
		{
			var requestPath = context.Request.Path.ToString();

			Log.Debug("{path}", requestPath);

			if (requestPath.StartsWith("/v3") == false)
			{
				return Next.Invoke(context);
			}

			var client = new HttpClient()
			{
				BaseAddress = new Uri("http://api.nuget.org")
			};

			var response = client.GetAsync(requestPath).Result;

			context.Response.ContentType = response.Content.Headers.ContentType.MediaType;

			using (var sr = new StreamReader(response.Content.ReadAsStreamAsync().Result))
			using (var sw = new StreamWriter(context.Response.Body))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					sw.WriteLine(line.Replace("https://api.nuget.org/", "http://localhost:55628/"));
				}
			}

			return Task.Delay(0);
		}
	}
}
