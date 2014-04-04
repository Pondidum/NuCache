using System.Web.Http;
using System.Web.Mvc;
using NuCache.App_Start;
using StructureMap;
using StructureMap.Graph;

namespace NuCache
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			var container = new Container(c =>
				c.Scan(a =>
				{
					a.TheCallingAssembly();
					a.LookForRegistries();
				}));

			config.DependencyResolver = new StructureMapDependencyResolver(container);

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
				name: "Search",
				routeTemplate: "api/v2/search()",
				defaults: new {controller = "Packages", action = "Search"}
			);

			config.Routes.MapHttpRoute(
				name: "Download",
				routeTemplate: "api/v2/package/{packageID}/{version}",
				defaults: new { controller = "Packages", action = "GetPackageByID", packageID = UrlParameter.Optional, version = UrlParameter.Optional}
			);
		}
	}
}
