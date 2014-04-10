using System;
using System.IO;

namespace NuCache
{
	public interface IPackageCache
	{
		bool Contains(string name, string version);
		void Store(string name, string version, Stream contents);
		Stream Get(string name, string version);
		void Remove(string name, string version);
		void RemoveAll();
	}
}
