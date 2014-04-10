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
			if (response.Content.Headers.ContentType.MediaType != "application/zip")
			{
				return;
			}

			//not certain why this gets missed by the web client on a download
			var name = Path.GetFileName(response.RequestMessage.RequestUri.AbsolutePath);
			response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = name };
		}
	}
}
