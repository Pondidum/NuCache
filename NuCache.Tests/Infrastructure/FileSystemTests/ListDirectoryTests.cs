﻿using System;
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
			Directory.Delete(DirectoryName);

			Assert.Throws<DirectoryNotFoundException>(() => FileSystem.ListDirectory(DirectoryName));
		}

		[Fact]
		public void When_listing_an_empty_directory()
		{
			FileSystem.ListDirectory(DirectoryName).ShouldBeEmpty();
		}

		[Fact]
		public void When_listing_a_directory_with_2_items()
		{
			File.Create(Path.Combine(DirectoryName, Guid.NewGuid().ToString() + ".tmp")).Close();
			File.Create(Path.Combine(DirectoryName, Guid.NewGuid().ToString() + ".tmp")).Close();

			FileSystem.ListDirectory(DirectoryName).Count().ShouldEqual(2);
		}
	}
}
