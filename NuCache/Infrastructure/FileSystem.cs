using System.Collections.Generic;
using System.IO;

namespace NuCache.Infrastructure
{
	public class FileSystem : IFileSystem
	{
		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		public void WriteFile(string path, Stream contents)
		{
			using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				contents.CopyTo(fs);
			}
		}

		public void AppendFile(string path, Stream contents)
		{
			using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write))
			{
				contents.CopyTo(fs);
			}
		}

		public Stream ReadFile(string path)
		{
			return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		public void DeleteFile(string path)
		{
			File.Delete(path);
		}

		public bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		public void CreateDirectory(string path)
		{
			Directory.CreateDirectory(path);
		}

		public IEnumerable<string> ListDirectory(string path)
		{
			return Directory.GetFileSystemEntries(path);
		}

		public void DeleteDirectory(string path)
		{
			Directory.Delete(path, true);
		}
	}
}
