using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NuCache.Infrastructure;

namespace NuCache.PackageSources
{
	public class RemotePackageSource : IPackageSource
	{
		private readonly Uri _remoteUrl;
		private readonly WebClient _client;

		public RemotePackageSource(WebClient client, Uri remoteUrl)
		{
			_remoteUrl = remoteUrl;
			_client = client;
		}

		private Uri BuildUri(Uri input)
		{
			var builder = new UriBuilder(input);

			builder.Scheme = _remoteUrl.Scheme;
			builder.Host = _remoteUrl.Host;
			builder.Port = -1;

			return builder.Uri;
		}

		public async Task<HttpResponseMessage> Get(Uri request)
		{
			return await _client.GetResponseAsync(BuildUri(request));
		}

		public async Task<HttpResponseMessage> Metadata(Uri request)
		{
			return await _client.GetResponseAsync(BuildUri(request));
		}

		public async Task<HttpResponseMessage> List(Uri request)
		{
			return await _client.GetResponseAsync(BuildUri(request));
		}

		public async Task<HttpResponseMessage> Search(Uri request)
		{
			return await _client.GetResponseAsync(BuildUri(request));
		}

		public async Task<HttpResponseMessage> FindPackagesByID(Uri request)
		{
			return await _client.GetResponseAsync(BuildUri(request));
		}

		public async Task<HttpResponseMessage> GetPackageByID(Uri request)
		{
			var result = await _client.GetResponseAsync(BuildUri(request));

			//not certain why this gets missed by the web client on a download
			var name = Path.GetFileName(result.RequestMessage.RequestUri.AbsolutePath);
			result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = name };

			return result;
		}
	}
}
