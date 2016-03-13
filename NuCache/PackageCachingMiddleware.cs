using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin;
using Serilog;

namespace NuCache
{
	public class PackageCachingMiddleware : OwinMiddleware
	{
		private readonly PackageCache _cache;
		private static readonly ILogger Log = Serilog.Log.ForContext<UrlRewriteMiddlware>();
		

		public PackageCachingMiddleware(OwinMiddleware next, PackageCache cache) : base(next)
		{
			_cache = cache;
		}

		public override Task Invoke(IOwinContext context)
		{
			var requestPath = context.Request.Path.ToString();

			Log.Debug("{path}", requestPath);

			if (requestPath.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase) == false)
			{
				return Next.Invoke(context);
			}

			var packageName = Path.GetFileName(requestPath);

			if (_cache.Contains(packageName))
			{
				context.Response.ContentType = "binary/octet-stream";
				context.Response.Write(_cache.GetPackage(packageName));

				return Task.Delay(0);
			}

			var client = new HttpClient()
			{
				BaseAddress = new Uri("http://api.nuget.org")
			};

			var response = client.GetAsync(requestPath).Result;

			if (response.IsSuccessStatusCode == false)
			{
				context.Response.StatusCode = (int) response.StatusCode;
				return Task.Delay(0);
			}

			var bytes = response.Content.ReadAsByteArrayAsync().Result;

			_cache.StorePackage(packageName, bytes);

			context.Response.ContentType = response.Content.Headers.ContentType.MediaType;
			context.Response.Write(bytes);

			return Task.Delay(0);
		}
	}
}
