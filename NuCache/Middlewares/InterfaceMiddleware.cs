using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Owin.Routing;

namespace NuCache.Middlewares
{
	public class InterfaceMiddleware
	{
		private readonly Statistics _stats;
		private readonly JsonSerializerSettings _settings;

		public InterfaceMiddleware(Statistics stats)
		{
			_stats = stats;
			_settings = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};
		}

		public void Configuration(IAppBuilder app)
		{
			app.Get("/api/stats", async context =>
			{
				await context.WriteJson(_stats.ForAll(), _settings);
			});
		}
	}
}
