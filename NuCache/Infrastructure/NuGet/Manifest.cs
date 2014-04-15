using System.IO;
using System.Xml.Linq;

namespace NuCache.Infrastructure.NuGet
{
	public class Manifest
	{
		public string Name { get; private set; }
		public string Version { get; private set; }
		public PackageID ID { get; private set; }

		public Manifest(Stream stream)
		{
			var doc = XDocument.Load(stream);
			var ns = doc.Root.Name.Namespace;

			var manifest = doc.Root.Element(ns + "metadata");

			Name = manifest.Element(ns + "id").Value;
			Version = manifest.Element(ns + "version").Value;
			ID = new PackageID(Name, Version);

			//dont care about the rest for now
		}
	}
}
