using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Linq;
using NuCache.Infrastructure;

namespace NuCache.Controllers
{
	public class PackagesController : ApiController
	{
		private readonly PackageCache _cache;
		private readonly WebClient _client;

		public PackagesController(PackageCache cache, WebClient client)
		{
			_cache = cache;
			_client = client;
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Get()
		{
			return await _client.GetResponseAsync(new Uri("http://www.nuget.org/api/v2/"));
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Metadata()
		{
			return await _client.GetResponseAsync(new Uri("http://www.nuget.org/api/v2/$metadata"));
		}

		[HttpGet]
		public async Task<HttpResponseMessage> List()
		{
			return await _client.GetResponseAsync(new Uri("http://www.nuget.org/api/v2/Packages"));
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Search()
		{
			var builder = new UriBuilder(Request.RequestUri);
			builder.Host = "nuget.org";
			builder.Port = -1;

			return await _client.GetResponseAsync(builder.Uri);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> FindPackagesByID(string id)
		{
			return await _client.GetResponseAsync(new Uri("http://www.nuget.org/api/v2/FindPackagesById()?id=" + id));
		}

		[HttpGet]
		public async Task<HttpResponseMessage> GetPackageByID(string packageID, string version)
		{
			var url = new Uri("http://www.nuget.org/api/v2/Package/" + packageID + "/" + version);
			var result = await _client.GetResponseAsync(url);

			var name = Path.GetFileName(result.RequestMessage.RequestUri.AbsolutePath);
			result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = name};

			return result;
		}
	}
}
