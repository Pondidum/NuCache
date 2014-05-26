using System;
using NuCache.Infrastructure.NuGet;
using Should;
using Xunit;

namespace NuCache.Tests.Infrastructure.NuGet.PackageIDTests
{
	public class UriParsingTests
	{
		[Fact]
		public void When_parsing_a_package_specific_url()
		{
			var id = PackageID.FromPackageIDRequest(new Uri("http://localhost:42174/api/v2/package/jQuery/2.1.0/"));

			id.Name.ShouldEqual("jQuery");
			id.Version.ShouldEqual("2.1.0");
		}

		[Fact]
		public void When_parsing_a_pacakges_query_url()
		{
			var id = PackageID.FromPackageIDRequest(new Uri("http://localhost:42174/api/v2/Packages(Id='Aspose.Words',Version='11.1.0')"));

			id.Name.ShouldEqual("Aspose.Words");
			id.Version.ShouldEqual("11.1.0");
		}
	}
}
