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
			var model = new HomeViewModel();

			// HttpRouteCollection throws exceptions if you call .ToList() or .ToArray() etc
			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (var route in GlobalConfiguration.Configuration.Routes)
			{
				if (string.IsNullOrWhiteSpace(route.RouteTemplate) == false)
				{
					model.Routes.Add(route);
				}
			}

			return _responseFactory.From(model);
		}
	}
}
