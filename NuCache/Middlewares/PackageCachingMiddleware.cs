using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin;
using Serilog;

namespace NuCache.Middlewares
{
	public class PackageCachingMiddleware : OwinMiddleware
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<UrlRewriteMiddlware>();

		private readonly Configuration _config;
		private readonly PackageCache _cache;
		private readonly Statistics _stats;

		public PackageCachingMiddleware(OwinMiddleware next, Configuration config, PackageCache cache, Statistics stats) : base(next)
		{
			_config = config;
			_cache = cache;
			_stats = stats;
		}

		public override Task Invoke(IOwinContext context)
		{
			var requestPath = context.Request.Path.ToString();

			if (requestPath.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase) == false)
			{
				return Next.Invoke(context);
			}

			Log.Debug("{path}", requestPath);

			var packageName = Path.GetFileName(requestPath);

			if (_cache.Contains(packageName))
			{
				_stats.Add(packageName, context.Request.RemoteIpAddress);

				Log.Information("Got {packageName} from the cache", packageName);
				context.Response.ContentType = "binary/octet-stream";
				context.Response.Write(_cache.GetPackage(packageName));

				return Task.Delay(0);
			}

			var client = new HttpClient
			{
				BaseAddress = _config.SourceNugetFeed
			};

			var response = client.GetAsync(requestPath).Result;

			if (response.IsSuccessStatusCode == false)
			{
				Log.Information("Unable to find {packageName} in the source feed", packageName);
				context.Response.StatusCode = (int) response.StatusCode;
				return Task.Delay(0);
			}

			Log.Information("Got {packageName} from the source feed", packageName);

			var bytes = response.Content.ReadAsByteArrayAsync().Result;

			_cache.StorePackage(packageName, bytes);

			context.Response.ContentType = response.Content.Headers.ContentType.MediaType;
			context.Response.Write(bytes);

			return Task.Delay(0);
		}
	}
}
