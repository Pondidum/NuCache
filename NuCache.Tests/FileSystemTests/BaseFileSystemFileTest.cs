using System;
using System.IO;

namespace NuCache.Tests.FileSystemTests
{
	public class BaseFileSystemFileTest	 : IDisposable
	{
		protected string Filename;
		protected FileSystem FileSystem;

		public BaseFileSystemFileTest()
		{
			FileSystem = new FileSystem();

			Filename = Guid.NewGuid().ToString() + ".tmp";
			
			File.Create(Filename).Close();
		}

		public void Dispose()
		{
			try
			{
				if (File.Exists(Filename))
				{
					File.Delete(Filename);
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Enable to delete '{0}'", Filename);
			}

		}
	}
}
