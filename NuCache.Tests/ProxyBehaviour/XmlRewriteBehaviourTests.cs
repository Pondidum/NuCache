using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using NSubstitute;
using NuCache.ProxyBehaviour;
using NuCache.Rewriters;
using StructureMap;
using Xunit;

namespace NuCache.Tests.ProxyBehaviour
{
	public class XmlRewriteBehaviourTests
	{

		private static void ExecuteFor(Uri request, HttpResponseMessage response)
		{
			var container = new Container(new RewriterRegistry());

			var transformer = container.GetInstance<XmlRewriter>();
			var action = new XmlRewriteBehaviour(transformer);

			action.Execute(request, response);
		}

		private static HttpResponseMessage BuildContent(string contentType)
		{
			var content = new StreamContent(new MemoryStream());
			content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

			var response = Substitute.For<HttpResponseMessage>();
			response.Content = content;

			return response;
		}

		[Fact]
		public void When_the_response_has_the_correct_headers()
		{
			var url = new Uri("http://example.com");
			var response = BuildContent("application/atom+xml");

			ExecuteFor(url, response);

			response.Received(2).Content = Arg.Any<HttpContent>();
		}

		[Fact]
		public void When_the_response_has_different_cased_headers()
		{
			var url = new Uri("http://example.com");
			var response = BuildContent("APPLICATION/atom+xml");

			ExecuteFor(url, response);

			response.Received(2).Content = Arg.Any<HttpContent>();
		}

		[Fact]
		public void When_the_response_has_non_matching_headers()
		{
			var url = new Uri("http://example.com");
			var response = BuildContent("text/plain");

			ExecuteFor(url, response);

			response.Received(2).Content = Arg.Any<HttpContent>();
		}

	}
}
