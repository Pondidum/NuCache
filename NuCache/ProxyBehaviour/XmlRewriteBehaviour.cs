using System;
using System.Net.Http;
using System.Threading.Tasks;
using NuCache.Rewriters;

namespace NuCache.ProxyBehaviour
{
	public class XmlRewriteBehaviour : IProxyBehaviour
	{
		private readonly XmlRewriter _xmlRewriter;

		public XmlRewriteBehaviour(XmlRewriter xmlRewriter)
		{
			_xmlRewriter = xmlRewriter;
		}

		public async void Execute(HttpRequestMessage request, HttpResponseMessage response)
		{
			if (response.Content.Headers.ContentType.MediaType == "application/atom+xml")
			{
				response.Content = await TransformContent(request, response.Content);
			}
		}

		private async Task<HttpContent> TransformContent(HttpRequestMessage request, HttpContent inputContent)
		{
			var inputStream = await inputContent.ReadAsStreamAsync();
			var pushContent = new PushStreamContent((outputStream, content, context) =>
			{
				_xmlRewriter.Rewrite(request.RequestUri, inputStream, outputStream);
				outputStream.Close();
				inputStream.Close();
			});

			pushContent.Headers.ContentType = inputContent.Headers.ContentType;

			return pushContent;
		}
	}
}
