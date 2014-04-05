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
		public HttpResponseMessage Get()
		{
			var xml = _client.MakeRequest(new Uri("http://www.nuget.org/api/v2/"));

			return new HttpResponseMessage
			{
				RequestMessage = Request,
				Content = new XmlContent(XDocument.Parse(xml)),
			};
		}

		[HttpGet]
		public HttpResponseMessage Metadata()
		{
			var xml = _client.MakeRequest(new Uri("http://www.nuget.org/api/v2/$metadata"));

			return new HttpResponseMessage
			{
				RequestMessage = Request,
				Content = new XmlContent(XDocument.Parse(xml))
			};
		}

		[HttpGet]
		public HttpResponseMessage List()
		{
			var xml = _client.MakeRequest(new Uri("http://www.nuget.org/api/v2/Packages"));

			return new HttpResponseMessage
			{
				RequestMessage = Request,
				Content = new XmlContent(XDocument.Parse(xml)),
			};
		}

		//$count?$filter=IsLatestVersion&searchTerm=%27%27&targetFramework=%27net45%27&includePrerelease=false

		[HttpGet]
		public async Task<HttpResponseMessage> Search()
		{

			var builder = new UriBuilder(Request.RequestUri);
			builder.Host = "nuget.org";
			builder.Port = -1;

			var result = await _client.GetResponseAsync(builder.Uri);

			return result;
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
