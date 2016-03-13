using Microsoft.Owin;
using NuCache;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace NuCache
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.Use(typeof (ProxyingMiddlware));
		}
	}
}
