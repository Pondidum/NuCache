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
		private static readonly ILogger Log = Serilog.Log.ForContext<UrlRewriteMiddlware>();
		

		public PackageCachingMiddleware(OwinMiddleware next) : base(next)
		{
		}

		public override Task Invoke(IOwinContext context)
		{
			var requestPath = context.Request.Path.ToString();

			Log.Debug("{path}", requestPath);

			if (requestPath.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase) == false)
			{
				return Next.Invoke(context);
			}

			//var packageName = Path.GetFileName(requestPath);
			//_cache.Contains(packageName);

			var client = new HttpClient()
			{
				BaseAddress = new Uri("http://api.nuget.org")
			};

			var response = client.GetAsync(requestPath).Result;

			var bytes = response.Content.ReadAsByteArrayAsync().Result;

			context.Response.ContentType = response.Content.Headers.ContentType.MediaType;
			context.Response.Write(bytes);
			

			return Task.Delay(0);
		}
	}

	public interface IPackageCache
	{

	}
}
