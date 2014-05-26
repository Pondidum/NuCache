using System;
using System.IO;
using System.Linq;
using Packaging = System.IO.Packaging.Package;

namespace NuCache.Infrastructure.NuGet
{
	public class Package
	{
		internal const string PackageRelationshipNamespace = "http://schemas.microsoft.com/packaging/2010/07/";
		internal const string ManifestRelationType = "manifest";

		public Manifest Metadata { get; set; }

		public Package(Stream stream)
		{

			var package = Packaging.Open(stream);

			var relationshipType = package.GetRelationshipsByType(PackageRelationshipNamespace + ManifestRelationType).SingleOrDefault();
			var manifestPart = package.GetPart(relationshipType.TargetUri);

			using (var manifestStream = manifestPart.GetStream())
			{
				Metadata = new Manifest(manifestStream);
			}
		}

		public static LoadStatus<Package> TryLoadPackage(Stream stream)
		{
			try
			{
				return new LoadStatus<Package>(new Package(stream), true);
			}
			catch (Exception)
			{
				return new LoadStatus<Package>(null, false);
			}
		}
	}
}
