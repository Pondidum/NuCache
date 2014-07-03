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
			Directory.Delete(DirectoryPath);

			FileSystem.DirectoryExists(DirectoryPath).ShouldBeFalse();
		}

		[Fact]
		public void When_passed_a_relative_directory_path_and_the_directory_exists()
		{
			FileSystem.DirectoryExists(DirectoryPath).ShouldBeTrue();
		}

		[Fact]
		public void When_passed_an_absolute_directory_path_and_the_directory_doesnt_exist()
		{
			Directory.Delete(DirectoryPath);

			var absolute = Path.GetFullPath(DirectoryPath);
			FileSystem.DirectoryExists(absolute).ShouldBeFalse();
		}

		[Fact]
		public void When_passed_an_absolute_directory_path_and_the_directory_exists()
		{
			var absolute = Path.GetFullPath(DirectoryPath);
			FileSystem.DirectoryExists(absolute).ShouldBeTrue();
		}
	}
}
