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

		private readonly WebClient _client;
		private readonly UriHostTransformer _transformer;

		public RemotePackageSource(WebClient client, UriHostTransformer transformer)
		{
			_client = client;
			_transformer = transformer;
		}

		public async Task<HttpResponseMessage> Get(Uri request)
		{
			return await _client.GetResponseAsync(_transformer.Transform(request));
		}

		public async Task<HttpResponseMessage> Metadata(Uri request)
		{
			return await _client.GetResponseAsync(_transformer.Transform(request));
		}

		public async Task<HttpResponseMessage> List(Uri request)
		{
			return await _client.GetResponseAsync(_transformer.Transform(request));
		}

		public async Task<HttpResponseMessage> Search(Uri request)
		{
			return await _client.GetResponseAsync(_transformer.Transform(request));
		}

		public async Task<HttpResponseMessage> FindPackagesByID(Uri request)
		{
			return await _client.GetResponseAsync(_transformer.Transform(request));
		}

		public async Task<HttpResponseMessage> GetPackageByID(Uri request)
		{
			var result = await _client.GetResponseAsync(_transformer.Transform(request));

			//not certain why this gets missed by the web client on a download
			var name = Path.GetFileName(result.RequestMessage.RequestUri.AbsolutePath);
			result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = name };

			return result;
		}
	}
}
