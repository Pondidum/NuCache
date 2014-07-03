using System;
using System.IO;
using Should;
using Xunit;
using Assert = Should.Core.Assertions.Assert;

namespace NuCache.Tests.Infrastructure.FileSystemTests
{
	public class DeleteDirectoryTests : BaseFileSystemDirectoryTest
	{
		[Fact]
		public void When_deleting_a_non_existing_directory()
		{
			Directory.Delete(DirectoryPath);

			Assert.Throws<DirectoryNotFoundException>(() => FileSystem.DeleteDirectory(DirectoryPath));
		}

		[Fact]
		public void When_deleting_an_empty_directory()
		{
			FileSystem.DeleteDirectory(DirectoryPath);

			Directory.Exists(DirectoryPath).ShouldBeFalse();
			
		}

		[Fact]
		public void When_deleting_a_directory_with_contents()
		{
			File.Create(Path.Combine(DirectoryPath, Guid.NewGuid().ToString() + ".tmp")).Close();
			FileSystem.DeleteDirectory(DirectoryPath);

			Directory.Exists(DirectoryPath).ShouldBeFalse();
		}
	}
}
