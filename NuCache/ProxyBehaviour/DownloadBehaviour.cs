using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NuCache.ProxyBehaviour
{
	public class DownloadBehaviour : IProxyBehaviour
	{
		public void Execute(Uri request, HttpResponseMessage response)
		{
			var headers = response.Content.Headers;

			if (String.Equals(headers.ContentType.MediaType, "application/zip", StringComparison.OrdinalIgnoreCase))
			{
				//not certain why this gets missed by the web client on a download
				var name = Path.GetFileName(response.RequestMessage.RequestUri.AbsolutePath);
				headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = name };

			}
		}
	}
}
