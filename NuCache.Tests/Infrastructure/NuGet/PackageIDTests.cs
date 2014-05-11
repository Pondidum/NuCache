using NuCache.Infrastructure.NuGet;
using Should;
using Xunit;

namespace NuCache.Tests.Infrastructure.NuGet
{
	public class PackageIDTests
	{
		[Fact]
		public void When_two_instances_are_compared_and_name_and_version_match()
		{
			var first = new PackageID("Testing", "1.2.3");
			var second = new PackageID("Testing", "1.2.3");

			first.ShouldEqual(second);
		}

		[Fact]
		public void When_two_instances_are_compared_and_only_name_matches()
		{
			var first = new PackageID("Testing", "2.4.8");
			var second = new PackageID("Testing", "1.2.3");

			first.ShouldNotEqual(second);
		}

		[Fact]
		public void When_two_instances_are_compared_and_only_version_matches()
		{
			var first = new PackageID("Testing", "1.2.3");
			var second = new PackageID("NoMatch", "1.2.3");

			first.ShouldNotEqual(second);

		}
	}
}
