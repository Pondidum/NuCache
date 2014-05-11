using System.IO;
using Should;
using Xunit;

namespace NuCache.Tests.Infrastructure.FileSystemTests
{
	public class DirectoryExistsTests : BaseFileSystemDirectoryTest
	{
		[Fact]
		public void When_passed_a_blank_directory_path()
		{
			FileSystem.DirectoryExists(string.Empty).ShouldBeFalse();
		}

		[Fact]
		public void When_passed_a_relative_directory_path_and_the_directory_doesnt_exist()
		{
			Directory.Delete(DirectoryName);

			FileSystem.DirectoryExists(DirectoryName).ShouldBeFalse();
		}

		[Fact]
		public void When_passed_a_relative_directory_path_and_the_directory_exists()
		{
			FileSystem.DirectoryExists(DirectoryName).ShouldBeTrue();
		}

		[Fact]
		public void When_passed_an_absolute_directory_path_and_the_directory_doesnt_exist()
		{
			Directory.Delete(DirectoryName);

			var absolute = Path.GetFullPath(DirectoryName);
			FileSystem.DirectoryExists(absolute).ShouldBeFalse();
		}

		[Fact]
		public void When_passed_an_absolute_directory_path_and_the_directory_exists()
		{
			var absolute = Path.GetFullPath(DirectoryName);
			FileSystem.DirectoryExists(absolute).ShouldBeTrue();
		}
	}
}
