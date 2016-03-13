using System;
using System.IO;
using System.Net.Http;
using Microsoft.Owin;
using NuCache.Properties;
using Owin;
using Owin.Routing;

[assembly:OwinStartup(typeof(Startup))]

namespace NuCache.Properties
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var rootUrl = new Uri("http://api.nuget.org");

			app.Use(async (context, next) =>
			{
				if (context.Request.Path.ToString() != "/v3/index.json")
					await next();
				else
					await context.Response.WriteAsync("[]");
			});

			app.Use(async (context, next) =>
			{
				var client = new HttpClient()
				{
					BaseAddress = rootUrl
				};

				var response = await client.GetAsync(context.Request.Path.ToString());

				using (var sr = new StreamReader(await response.Content.ReadAsStreamAsync()))
				using (var sw = new StreamWriter(context.Response.Body))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						sw.WriteLine(line);
					}
				}
			});
		} 
	}
}
