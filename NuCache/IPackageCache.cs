using System.IO;

namespace NuCache
{
	public interface IPackageCache
	{
		void Initialise();

		bool Contains(PackageID packageID);
		void Store(PackageID packageID, Stream contents);
		Stream Get(PackageID packageID);
		void Remove(PackageID packageID);
		void RemoveAll();

	}
}
