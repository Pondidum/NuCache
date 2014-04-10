using System;
using System.Net.Http;
using System.Threading.Tasks;
using NuCache.Infrastructure;
using NuCache.ProxyBehaviour;
using NuCache.Rewriters;

namespace NuCache.PackageSources
{
	public class ProxyingPackageSource : IPackageSource
	{
		private readonly ApplicationSettings _settings;
		private readonly WebClient _client;
		private readonly ProxyBehaviourSet _behaviours;
		private readonly UriRewriter _transformer;

		public ProxyingPackageSource(ApplicationSettings settings, WebClient client, ProxyBehaviourSet behaviours, UriRewriter transformer)
		{
			_settings = settings;
			_client = client;
			_behaviours = behaviours;
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

		public async Task<HttpResponseMessage> GetPackageByID(Uri request)
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
