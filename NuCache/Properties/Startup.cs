using Microsoft.Owin;
using NuCache.Properties;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace NuCache.Properties
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.Use(typeof (ProxyingMiddlware));
		}
	}
}
