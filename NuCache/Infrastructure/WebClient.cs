using System;
using System.IO;
using System.Net;

namespace NuCache.Infrastructure
{
	public class WebClient
	{
		public string MakeRequest(Uri url)
		{
			var request = (HttpWebRequest) WebRequest.Create(url);
			request.ContentType = "text/xml";

			using (var response = request.GetResponse())
			using (var stream  = response.GetResponseStream())
			using (var reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}
	}
}
