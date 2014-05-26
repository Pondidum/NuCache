using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using NuCache.Infrastructure.Spark;
using NuCache.Models;

namespace NuCache.Controllers
{
	public class HomeController : ApiController
	{
		private readonly ApplicationSettings _settings;
		private readonly SparkResponseFactory _responseFactory;
		private readonly IPackageCache _packageCache;

		public HomeController(ApplicationSettings settings, SparkResponseFactory responseFactory, IPackageCache packageCache)
		{
			_settings = settings;
			_responseFactory = responseFactory;
			_packageCache = packageCache;
		}

		public HttpResponseMessage Get()
		{
			var model = new HomeViewModel();
			var api = new Uri(Request.RequestUri, _settings.ApiEndpoint);

			model.Packages.AddRange(_packageCache.GetAllPackages().ToList());
			model.ApiUrl = api;


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
