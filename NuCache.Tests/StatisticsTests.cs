using System.Linq;
using NSubstitute;
using NuCache.Infrastructure;
using Shouldly;
using Xunit;

namespace NuCache.Tests
{
	public class StatisticsTests
	{
		private readonly Statistics _stats;

		public StatisticsTests()
		{
			_stats = new Statistics(Substitute.For<IFileSystem>(), Substitute.For<IConfiguration>());
		}

		[Fact]
		public void When_there_are_no_stats()
		{
			_stats.ForAll().ShouldBeEmpty();
		}

		[Fact]
		public void When_there_is_one_package()
		{
			_stats.Add(PackageName.Parse("finite.4.1.0.nupkg"), "127.0.0.1");

			var stat = _stats.ForAll().Single();

			stat.ShouldSatisfyAllConditions(
				() => stat.Downloads.ShouldBe(1),
				() => stat.Name.ShouldBe("finite"),
				() => stat.Version.ShouldBe("4.1.0")
			);
		}

		[Fact]
		public void When_there_is_one_package_multiple_times()
		{
			_stats.Add(PackageName.Parse("finite.4.1.0.nupkg"), "127.0.0.1");
			_stats.Add(PackageName.Parse("finite.4.1.0.nupkg"), "127.0.0.1");
			_stats.Add(PackageName.Parse("finite.4.1.0.nupkg"), "127.0.0.1");

			var stat = _stats.ForAll().Single();

			stat.ShouldSatisfyAllConditions(
				() => stat.Downloads.ShouldBe(3),
				() => stat.Name.ShouldBe("finite"),
				() => stat.Version.ShouldBe("4.1.0")
			);
		}

		[Fact]
		public void When_there_is_one_package_with_multiple_versions()
		{
			_stats.Add(PackageName.Parse("finite.3.5.8.nupkg"), "127.0.0.1");
			_stats.Add(PackageName.Parse("finite.4.1.0.nupkg"), "127.0.0.1");
			_stats.Add(PackageName.Parse("finite.4.1.0.nupkg"), "127.0.0.1");

			var v3 = _stats.ForAll().Single(s => s.Version == "3.5.8");
			var v4 = _stats.ForAll().Single(s => s.Version == "4.1.0");

			v3.ShouldSatisfyAllConditions(
				() => v3.Downloads.ShouldBe(1),
				() => v3.Name.ShouldBe("finite"),
				() => v3.Version.ShouldBe("3.5.8")
			);

			v4.ShouldSatisfyAllConditions(
				() => v4.Downloads.ShouldBe(2),
				() => v4.Name.ShouldBe("finite"),
				() => v4.Version.ShouldBe("4.1.0")
			);
		}
	}
}
