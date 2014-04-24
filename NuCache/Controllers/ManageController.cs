﻿using System.Net;
using System.Net.Http;
using System.Web.Http;
using NuCache.Infrastructure.Spark;
using NuCache.Models;

namespace NuCache.Controllers
{
	public class ManageController : ApiController
	{
		private readonly SparkResponseFactory _responseFactory;
		private readonly IPackageCache _packageCache;

		public ManageController(SparkResponseFactory responseFactory, IPackageCache packageCache)
		{
			_responseFactory = responseFactory;
			_packageCache = packageCache;
		}

		public HttpResponseMessage GetIndex()
		{
			var model = new ManageViewModel
			{
				Packages = _packageCache.GetAllPackages()
			};

			return _responseFactory.From(model);

		}
	}
}
