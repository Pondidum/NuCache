using System;
using System.IO;
using NuCache.Infrastructure;

namespace NuCache.Tests.Infrastructure.FileSystemTests
{
	public class BaseFileSystemDirectoryTest : IDisposable
	{
		protected string DirectoryPath;
		protected FileSystem FileSystem;

		public BaseFileSystemDirectoryTest()
		{
			FileSystem = new FileSystem();

			DirectoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

			Directory.CreateDirectory(DirectoryPath);
		}

		public void Dispose()
		{
			try
			{
				if (Directory.Exists(DirectoryPath))
				{
					Directory.Delete(DirectoryPath, true);
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Enable to delete '{0}'", DirectoryPath);
			}

		}
	}
}