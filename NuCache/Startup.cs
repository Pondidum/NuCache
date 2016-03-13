using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using NuCache;
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

			app.Use<SerilogMiddleware>();
			app.Use<PackageCachingMiddleware>();
			app.Use<UrlRewriteMiddlware>();

			app.Run(context =>
			{
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;
				return Task.Delay(0);
			});

		}
	}
}
