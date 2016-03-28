using System.Collections.Generic;

namespace NuCache.Infrastructure
{
	public interface IFileSystem
	{
		bool FileExists(string filePath);
		void AppendToFile(string filePath, string line);
		IEnumerable<string> ReadFileLines(string filePath);
	}
}
