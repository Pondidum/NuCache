using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using NuCache.Infrastructure.NuGet;

namespace NuCache.Models
{
	public class HomeViewModel
	{
		public List<IHttpRoute> Routes { get; private set; }
		public List<PackageID> Packages { get; private set; }
		public Uri ApiUrl { get; set; }

		public HomeViewModel()
		{
			Routes = new List<IHttpRoute>();
			Packages = new List<PackageID>();
		}
	}
}
