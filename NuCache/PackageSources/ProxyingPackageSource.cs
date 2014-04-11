using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NuCache.ProxyBehaviour;
using NuCache.Rewriters;
using WebClient = NuCache.Infrastructure.WebClient;

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

		public async Task<HttpResponseMessage> Get(Uri request)
		{
			return await HandleRequest(request);
		}

		public async Task<HttpResponseMessage> Metadata(Uri request)
		{
			return await HandleRequest(request);
		}

		public async Task<HttpResponseMessage> List(Uri request)
		{
			return await HandleRequest(request);
		}

		public async Task<HttpResponseMessage> Search(Uri request)
		{
			return await HandleRequest(request);
		}

		public async Task<HttpResponseMessage> FindPackagesByID(Uri request)
		{
			return await HandleRequest(request);
		}

		public async Task<HttpResponseMessage> GetPackageByID(Uri request, string packageID, string version)
		{
			HttpResponseMessage response;

			if (_cache.Contains(packageID, version))
			{
				var name = String.Format("{0}.{1}.nupkg", packageID, version);
				var stream = _cache.Get(packageID, version);

				response = _client.BuildDownloadResponse(request, stream, name);
			}
			else
			{
				response = await HandleRequest(request);
				_cache.Store(packageID, version, await response.Content.ReadAsStreamAsync());	
			}

			_behaviours.Execute(request, response);

			return response;
		}

		public async Task<HttpResponseMessage> GetPackageIDs(Uri request)
		{
			return await HandleRequest(request);
		}

		private async Task<HttpResponseMessage> HandleRequest(Uri request)
		{
			var targetUri = _transformer.TransformHost(_settings.RemoteFeed, request);
			var response = await _client.GetResponseAsync(targetUri);

			_behaviours.Execute(request, response);

			return response;	
		}
	}
}
