using System.Web.Http;
using NuCache.App_Start;
using StructureMap;
using StructureMap.Graph;

namespace NuCache
{
	public static class ConfigureContainer
	{
		public static void Register(HttpConfiguration config)
		{
			var container = new Container(c =>
				c.Scan(a =>
				{
					a.TheCallingAssembly();
					a.WithDefaultConventions();
					a.LookForRegistries();
				}));

			config.DependencyResolver = new StructureMapDependencyResolver(container);
		}
	}
}
