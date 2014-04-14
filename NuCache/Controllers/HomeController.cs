using System.Net.Http;
using System.Web.Http;
using NuCache.Infrastructure.Spark;
using NuCache.Models;

namespace NuCache.Controllers
{
	public class HomeController : ApiController
	{
		private readonly SparkResponseFactory _responseFactory;

		public HomeController(SparkResponseFactory responseFactory)
		{
			_responseFactory = responseFactory;
		}

		[HttpGet]
		public HttpResponseMessage GetIndex()
		{
			return _responseFactory.From(new HomeViewModel { Name = "Andy" });
		}
	}
}
