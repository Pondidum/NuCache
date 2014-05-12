using System.Collections.Generic;
using System.IO;

namespace NuCache.Infrastructure
{
	public interface IFileSystem
	{
		bool FileExists(string path);
		void WriteFile(string path, Stream contents);
		void AppendFile(string path, Stream contents);
		Stream ReadFile(string path);
		void DeleteFile(string path);

		bool DirectoryExists(string path);
		void CreateDirectory(string path);
		IEnumerable<string> ListDirectory(string path);
		void DeleteDirectory(string path);
	}
}
