using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NuCache.Infrastructure
{
	public class WebClient
	{
		public virtual async Task<HttpResponseMessage> GetResponseAsync(Uri url)
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, url);

			return await client.SendAsync(request);
		}

		public virtual HttpResponseMessage BuildDownloadResponse(Uri request, Stream stream, string name)
		{
			var content = new PushStreamContent((responseStream, cont, context) =>
			{
				stream.CopyTo(responseStream);

				responseStream.Close();
				stream.Close();
			});

			content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
			content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = name
			};

			var message = new HttpRequestMessage()
			{
				RequestUri = request
			};

			return new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = content,
				RequestMessage = message,
			};
		}
	}
}
