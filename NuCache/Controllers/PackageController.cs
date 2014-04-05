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

		private Uri BuildUri(Uri input)
		{
			var builder = new UriBuilder(input);
			builder.Host = "nuget.org";
			builder.Port = -1;

			return builder.Uri;
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Get()
		{
			return await _client.GetResponseAsync(BuildUri(Request.RequestUri));
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Metadata()
		{
			return await _client.GetResponseAsync(BuildUri(Request.RequestUri));
		}

		[HttpGet]
		public async Task<HttpResponseMessage> List()
		{
			return await _client.GetResponseAsync(BuildUri(Request.RequestUri));
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Search()
		{
			return await _client.GetResponseAsync(BuildUri(Request.RequestUri));
		}

		[HttpGet]
		public async Task<HttpResponseMessage> FindPackagesByID()
		{
			return await _client.GetResponseAsync(BuildUri(Request.RequestUri));
		}

		[HttpGet]
		public async Task<HttpResponseMessage> GetPackageByID()
		{
			var result = await _client.GetResponseAsync(BuildUri(Request.RequestUri));

			//not certain why this gets missed by the web client on a download
			var name = Path.GetFileName(result.RequestMessage.RequestUri.AbsolutePath);
			result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = name};

			return result;
		}
	}
}
