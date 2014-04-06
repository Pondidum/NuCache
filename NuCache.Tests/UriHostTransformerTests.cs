using System;
using Should;

namespace NuCache.Tests
{
	public class UriHostTransformerTests
	{

		public void When_changing_a_secure_host_to_a_non_secure_host()
		{
			var transformer = new UriHostTransformer(new Uri("http://example.com"));
			
			transformer
				.Transform(new Uri("https://example.com"))
				.ShouldEqual(new Uri("http://example.com"));
		}

		public void When_changing_a_non_secure_host_to_a_secure_host()
		{
			var transformer = new UriHostTransformer(new Uri("https://example.com"));

			transformer
				.Transform(new Uri("https://example.com"))
				.ShouldEqual(new Uri("https://example.com"));
		}

		public void When_changing_a_host_path_and_query_should_not_be_effected()
		{
			var transformer = new UriHostTransformer(new Uri("http://nuget.org"));

			transformer
				.Transform(new Uri("http://localhost.fiddler:42174/api/v2/search()/$count?offset=23"))
				.ShouldEqual(new Uri("http://nuget.org/api/v2/search()/$count?offset=23"));
		}
	}
}
