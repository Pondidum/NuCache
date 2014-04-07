using System;
using System.IO;

namespace NuCache.Tests.FileSystemTests
{
	public class BaseFileSystemDirectoryTest : IDisposable
	{
		protected string DirectoryName;
		protected FileSystem FileSystem;

		public BaseFileSystemDirectoryTest()
		{
			FileSystem = new FileSystem();

			DirectoryName = Guid.NewGuid().ToString();

			Directory.CreateDirectory(DirectoryName);
		}

		public void Dispose()
		{
			try
			{
				if (Directory.Exists(DirectoryName))
				{
					Directory.Delete(DirectoryName);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Enable to delete '{0}'", DirectoryName);
			}

		}
	}
}