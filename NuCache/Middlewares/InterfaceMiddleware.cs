using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NuCache.Infrastructure;
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

			var fs = new AssemblyResourceFileSystem(Assembly.GetExecutingAssembly(), "NuCache.client");

			var fileOptions = new FileServerOptions
			{
				FileSystem = fs,
				EnableDefaultFiles = true,
				DefaultFilesOptions = { DefaultFileNames = { "app.htm" } }
			};

			app.UseFileServer(fileOptions);
		}
	}
}
