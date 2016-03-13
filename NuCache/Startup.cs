using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using NuCache;
using NuCache.Middlewares;
using Owin;
using Serilog;

[assembly: OwinStartup(typeof(Startup))]

namespace NuCache
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var config = new Configuration
			{
				SourceNugetFeed = new Uri("https://api.nuget.org"),
				CacheDirectory = "cache"
			};

			var packageCache = new PackageCache(config.CacheDirectory);

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.Enrich.FromLogContext()
				.WriteTo.Trace()
				.CreateLogger();

			app.Use<SerilogMiddleware>();
			app.Use<PackageCachingMiddleware>(config, packageCache);
			app.Use<UrlRewriteMiddlware>(config);

			app.Run(context =>
			{
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;
				return Task.Delay(0);
			});

		}
	}
}
