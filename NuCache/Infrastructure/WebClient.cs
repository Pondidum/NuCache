using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NuCache.Infrastructure
{
	public class WebClient
	{
		public async Task<HttpResponseMessage> GetResponseAsync(Uri url)
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, url);

			return await client.SendAsync(request);
		}
	}
}
