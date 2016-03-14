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
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.Enrich.FromLogContext()
				.WriteTo.Trace()
				.CreateLogger();

			var config = new Configuration();
			var packageCache = new PackageCache(config.CacheDirectory);

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
