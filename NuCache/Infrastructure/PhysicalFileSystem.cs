using System.Collections.Generic;
using System.IO;

namespace NuCache.Infrastructure
{
	public class PhysicalFileSystem : IFileSystem
	{
		public bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}

		public void AppendToFile(string filePath, string line)
		{
			File.AppendAllLines(filePath, new []{ line });
		}

		public IEnumerable<string> ReadFileLines(string filePath)
		{
			return File.ReadAllLines(filePath);
		}
	}
}
