using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using NuCache.Infrastructure;
using NuCache.Rewriters;

namespace NuCache.PackageSources
{
	public class ProxyingPackageSource : IPackageSource
	{
		private readonly ApplicationSettings _settings;
		private readonly WebClient _client;
		private readonly XmlRewriter _xmlRewriter;
		private readonly UriRewriter _transformer;

		public ProxyingPackageSource(ApplicationSettings settings, WebClient client, XmlRewriter xmlRewriter, UriRewriter transformer)
		{
			_settings = settings;
			_client = client;
			_xmlRewriter = xmlRewriter;
			_transformer = transformer;
		}

		private Uri GetTargetUrl(Uri request)
		{
			return _transformer.TransformHost(_settings.RemoteFeed, request);
		}

		public async Task<HttpResponseMessage> Get(Uri request)
		{
			return await _client.GetResponseAsync(GetTargetUrl(request));
		}

		public async Task<HttpResponseMessage> Metadata(Uri request)
		{
			return await _client.GetResponseAsync(GetTargetUrl(request));
		}

		public async Task<HttpResponseMessage> List(Uri request)
		{
			var response = await _client.GetResponseAsync(GetTargetUrl(request));

			var inputStream = await response.Content.ReadAsStreamAsync();
			var pushContent = new PushStreamContent((outputStream, content, context) =>
			{
				_xmlRewriter.Rewrite(request, inputStream, outputStream);
				outputStream.Close();
				inputStream.Close();
			});

			pushContent.Headers.ContentType = response.Content.Headers.ContentType;
			response.Content = pushContent;

			return response;
		}

		public async Task<HttpResponseMessage> Search(Uri request)
		{
			return await _client.GetResponseAsync(GetTargetUrl(request));
		}

		public async Task<HttpResponseMessage> FindPackagesByID(Uri request)
		{
			return await _client.GetResponseAsync(GetTargetUrl(request));
		}

		public async Task<HttpResponseMessage> GetPackageByID(Uri request)
		{
			var result = await _client.GetResponseAsync(GetTargetUrl(request));

			//not certain why this gets missed by the web client on a download
			var name = Path.GetFileName(result.RequestMessage.RequestUri.AbsolutePath);
			result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = name };

			return result;
		}
	}
}
