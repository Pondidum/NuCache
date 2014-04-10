using System;
using System.IO;
using Should;
using Should.Core.Assertions;

namespace NuCache.Tests.Infrastructure.FileSystemTests
{
	public class DeleteDirectoryTests : BaseFileSystemDirectoryTest
	{
		public void When_deleting_a_non_existing_directory()
		{
			Directory.Delete(DirectoryName);

			Assert.Throws<DirectoryNotFoundException>(() => FileSystem.DeleteDirectory(DirectoryName));
		}

		public void When_deleting_an_empty_directory()
		{
			FileSystem.DeleteDirectory(DirectoryName);

			Directory.Exists(DirectoryName).ShouldBeFalse();
			
		}

		public void When_deleting_a_directory_with_contents()
		{
			File.Create(Path.Combine(DirectoryName, Guid.NewGuid().ToString() + ".tmp")).Close();
			FileSystem.DeleteDirectory(DirectoryName);

			Directory.Exists(DirectoryName).ShouldBeFalse();
		}
	}
}
