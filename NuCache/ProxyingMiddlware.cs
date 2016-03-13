using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace NuCache
{
	public class ProxyingMiddlware : OwinMiddleware
	{
		public ProxyingMiddlware(OwinMiddleware next) : base(next)
		{
		}

		public override Task Invoke(IOwinContext context)
		{
			if (context.Request.Path.ToString().StartsWith("/v3") == false)
			{
				return Next.Invoke(context);
			}

			var client = new HttpClient()
			{
				BaseAddress = new Uri("http://api.nuget.org")
			};

			var response = client.GetAsync(context.Request.Path.ToString()).Result;

			context.Response.ContentType = response.Content.Headers.ContentType.MediaType;

			using (var sr = new StreamReader(response.Content.ReadAsStreamAsync().Result))
			using (var sw = new StreamWriter(context.Response.Body))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					sw.WriteLine(line);
				}
			}

			return Task.Delay(0);
		}
	}
}
