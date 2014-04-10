using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using NSubstitute;
using NuCache.ProxyBehaviour;
using NuCache.Rewriters;
using Should;

namespace NuCache.Tests.ProxyBehaviour
{
	public class DownloadBehaviourTests
	{
		private static void ExecuteFor(Uri request, HttpResponseMessage response)
		{
			var action = new DownloadBehaviour();
			action.Execute(request, response);
		}

		private static HttpResponseMessage BuildContent(string contentType)
		{
			var content = new StreamContent(new MemoryStream());
			content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

			var request = Substitute.For<HttpRequestMessage>();
			request.RequestUri = new Uri("http://output.example.com/package/testpackage.1.0.0.nupkg");

			var response = Substitute.For<HttpResponseMessage>();
			response.Content = content;
			response.RequestMessage = request;

			return response;
		}


		public void When_the_response_has_the_correct_headers()
		{
			var url = new Uri("http://example.com");
			var response = BuildContent("application/zip");

			ExecuteFor(url, response);

			response.Content.Headers.ContentDisposition.FileName.ShouldEqual("testpackage.1.0.0.nupkg");
		}

		public void When_the_response_has_different_cased_headers()
		{
			var url = new Uri("http://example.com");
			var response = BuildContent("APPLICATION/ZIP");

			ExecuteFor(url, response);

			response.Content.Headers.ContentDisposition.FileName.ShouldEqual("testpackage.1.0.0.nupkg");
		}

		public void When_the_response_has_non_matching_headers()
		{
			var url = new Uri("http://example.com");
			var response = BuildContent("text/plain");

			ExecuteFor(url, response);

			response.Content.Headers.ContentDisposition.ShouldBeNull();
		}
	}
}
