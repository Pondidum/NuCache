using System.Web.Http;

namespace NuCache
{
	public static class ConfigureRoutes
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "Api",
				routeTemplate: "api/v2/{*url}",
				defaults: new { controller = "Packages", action = "Dispatch", url = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "Home",
				routeTemplate: "",
				defaults: new {controller = "Home"}
			);

			config.Routes.MapHttpRoute(
				name: "Standard",
				routeTemplate: "{controller}/{name}/{version}",
				defaults: new { name = RouteParameter.Optional, version = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "Error404",
				routeTemplate: "{*url}",
				defaults: new { controller = "Error", action = "Handle404" }
			);
		}
	}
}
