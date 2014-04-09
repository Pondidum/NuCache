using System;
using NuCache.Rewriters;
using Should;

namespace NuCache.Tests
{
	public class UriRewriterTests
	{
		public void When_changing_a_secure_host_to_a_non_secure_host()
		{
			var transformer = new UriRewriter();
			
			transformer
				.TransformHost(new Uri("http://example.com"), new Uri("https://example.com"))
				.ShouldEqual(new Uri("http://example.com"));
		}

		public void When_changing_a_non_secure_host_to_a_secure_host()
		{
			var transformer = new UriRewriter();

			transformer
				.TransformHost(new Uri("https://example.com"), new Uri("https://example.com"))
				.ShouldEqual(new Uri("https://example.com"));
		}

		public void When_changing_a_host_path_and_query_should_not_be_effected()
		{
			var transformer = new UriRewriter();

			transformer
				.TransformHost(new Uri("http://nuget.org"), new Uri("http://localhost.fiddler:42174/api/v2/search()/$count?offset=23"))
				.ShouldEqual(new Uri("http://nuget.org/api/v2/search()/$count?offset=23"));
		}

		public void When_changing_to_a_host_with_a_port()
		{
			var transformer = new UriRewriter();

			transformer
				.TransformHost(new Uri("http://localhost.fiddler:42174"), new Uri("https://example.com"))
				.ShouldEqual(new Uri("http://localhost.fiddler:42174"));
		}
	}
}
