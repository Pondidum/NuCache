using System.IO;

namespace NuCache
{
	public interface IPackageCache
	{
		bool Contains(string name, string version);
		void Write(string name, string version, Stream contents);
		void Remove(string name, string version);
		void RemoveAll();
	}
}
