using System;
using System.Web.Http;
using System.Web.Mvc;

namespace NuCache
{
	public static class ConfigureRoutes
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "Root",
				routeTemplate: "api/v2/",
				defaults: new { controller = "Packages", action = "Get"}
			);

			config.Routes.MapHttpRoute(
				name: "Metadata",
				routeTemplate: "api/v2/$metadata",
				defaults: new { controller = "Packages", action = "Metadata" }
			);

			config.Routes.MapHttpRoute(
				name: "Packages",
				routeTemplate: "api/v2/packages",
				defaults: new { controller = "Packages", action = "List"}
			);

			config.Routes.MapHttpRoute(
				name: "FindByID",
				routeTemplate: "api/v2/FindPackagesByID()",
				defaults: new { controller = "Packages", action = "FindPackagesByID" }
			);

			config.Routes.MapHttpRoute(
				name: "Search",
				routeTemplate: "api/v2/search()/{method}",
				defaults: new {controller = "Packages", action = "Search", method = UrlParameter.Optional}
			);

			config.Routes.MapHttpRoute(
				name: "Download",
				routeTemplate: "api/v2/package/{packageID}/{version}",
				defaults: new { controller = "Packages", action = "GetPackageByID", packageID = UrlParameter.Optional, version = UrlParameter.Optional}
			);

			config.Routes.MapHttpRoute(
				name: "package-ids",
				routeTemplate: "api/v2/package-ids",
				defaults: new { controller = "Packages", action = "GetPackageIDs" }
			);

			config.Routes.MapHttpRoute(
				name: "Home",
				routeTemplate: "",
				defaults: new {controller = "Home", action = "GetIndex"}
			);
		}

	}
}
