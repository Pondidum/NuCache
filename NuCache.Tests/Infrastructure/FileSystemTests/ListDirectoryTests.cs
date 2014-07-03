using System;
using System.IO;
using System.Linq;
using Should;
using Xunit;
using Assert = Should.Core.Assertions.Assert;

namespace NuCache.Tests.Infrastructure.FileSystemTests
{
	public class ListDirectoryTests : BaseFileSystemDirectoryTest
	{
		[Fact]
		public void When_listing_a_non_existing_directory()
		{
			Directory.Delete(DirectoryPath);

			Assert.Throws<DirectoryNotFoundException>(() => FileSystem.ListDirectory(DirectoryPath));
		}

		[Fact]
		public void When_listing_an_empty_directory()
		{
			FileSystem.ListDirectory(DirectoryPath).ShouldBeEmpty();
		}

		[Fact]
		public void When_listing_a_directory_with_2_items()
		{
			File.Create(Path.Combine(DirectoryPath, Guid.NewGuid().ToString() + ".tmp")).Close();
			File.Create(Path.Combine(DirectoryPath, Guid.NewGuid().ToString() + ".tmp")).Close();

			FileSystem.ListDirectory(DirectoryPath).Count().ShouldEqual(2);
		}
	}
}
