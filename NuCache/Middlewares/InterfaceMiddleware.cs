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
		private readonly PackageCache _cache;
		private readonly Statistics _stats;
		private readonly JsonSerializerSettings _settings;

		public InterfaceMiddleware(PackageCache cache, Statistics stats)
		{
			_cache = cache;
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

			app.Delete("/api/packages", async context =>
			{
				var dto = context.ReadJson<DeleteDto>();
				var package = new PackageName(dto.Name, dto.Version);

					_cache.RemovePackage(package);

				await Task.Yield();
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

		private class DeleteDto
		{
			public string Name { get; set; }
			public string Version { get; set; }
		}
	}
}
