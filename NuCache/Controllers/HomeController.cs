﻿using System.Linq;
using System.Net.Http;
using System.Web.Http;
using NuCache.Infrastructure.Spark;
using NuCache.Models;

namespace NuCache.Controllers
{
	public class HomeController : ApiController
	{
		private readonly SparkResponseFactory _responseFactory;
		private readonly IPackageCache _packageCache;

		public HomeController(SparkResponseFactory responseFactory, IPackageCache packageCache)
		{
			_responseFactory = responseFactory;
			_packageCache = packageCache;
		}

		[HttpGet]
		public HttpResponseMessage GetIndex()
		{
			var model = new HomeViewModel();

			model.Packages.AddRange(_packageCache.GetAllPackages().ToList());

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
