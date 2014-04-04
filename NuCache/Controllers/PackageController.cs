using System;
using System.Net.Http;
using System.Reflection;
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

		[HttpGet]
		public HttpResponseMessage Search()
		{
			var xml = _client.MakeRequest(new Uri("http://www.nuget.org/api/v2/Search()" + Request.RequestUri.Query));

			return new HttpResponseMessage
			{
				RequestMessage = Request,
				Content = new XmlContent(XDocument.Parse(xml))
			};
		}

		public void GetByPackageID(string packageID, string version)
		{
			if (_cache.ContainsPackage(packageID, version))
			{
				_cache.Package(packageID, version);  //return
			}

			//get from webclient
			//var package = _webClient.MakeRequest();

			//_cache.Add(package);
		}

	}
}
