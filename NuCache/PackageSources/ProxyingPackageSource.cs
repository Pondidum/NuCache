using System;
using System.Net.Http;
using System.Threading.Tasks;
using NuCache.Infrastructure;
using NuCache.Infrastructure.NuGet;
using NuCache.ProxyBehaviour;
using NuCache.Rewriters;

namespace NuCache.PackageSources
{
	public class ProxyingPackageSource : IPackageSource
	{
		private readonly ApplicationSettings _settings;
		private readonly WebClient _client;
		private readonly ProxyBehaviourSet _behaviours;
		private readonly IPackageCache _cache;
		private readonly UriRewriter _transformer;

		public ProxyingPackageSource(ApplicationSettings settings, WebClient client, ProxyBehaviourSet behaviours, IPackageCache cache, UriRewriter transformer)
		{
			_settings = settings;
			_client = client;
			_behaviours = behaviours;
			_cache = cache;
			_transformer = transformer;
		}

		public async Task<HttpResponseMessage> Get(HttpRequestMessage request)
		{
			return await HandleRequest(request);
		}

		public async Task<HttpResponseMessage> Metadata(HttpRequestMessage request)
		{
			return await HandleRequest(request);
		}

		public async Task<HttpResponseMessage> List(HttpRequestMessage request)
		{
			return await HandleRequest(request);
		}

		public async Task<HttpResponseMessage> Search(HttpRequestMessage request)
		{
			return await HandleRequest(request);
		}

		public async Task<HttpResponseMessage> FindPackagesByID(HttpRequestMessage request)
		{
			return await HandleRequest(request);
		}

		public async Task<HttpResponseMessage> GetPackageByID(HttpRequestMessage request, string name, string version)
		{
			HttpResponseMessage response;

			var packageID = new PackageID(name, version);

			if (_cache.Contains(packageID))
			{
				var stream = _cache.Get(packageID);

				response = _client.BuildDownloadResponse(request.RequestUri, stream, packageID.GetFileName());
			}
			else
			{
				response = await HandleRequest(request);
				_cache.Store(packageID, await response.Content.ReadAsStreamAsync());	
			}

			_behaviours.Execute(request, response);

			return response;
		}

		public async Task<HttpResponseMessage> GetPackageIDs(HttpRequestMessage request)
		{
			return await HandleRequest(request);
		}

		private async Task<HttpResponseMessage> HandleRequest(HttpRequestMessage request)
		{
			var targetUri = _transformer.TransformHost(_settings.RemoteFeed, request.RequestUri);
			var response = await _client.GetResponseAsync(targetUri);

			_behaviours.Execute(request, response);

			return response;	
		}
	}
}
