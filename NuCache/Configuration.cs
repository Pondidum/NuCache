using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace NuCache
{
	public class Configuration
	{
		public Uri SourceNugetFeed { get; set; }
		public string CacheDirectory { get; set; }

		public Configuration()
		{
			var directory = ConfigurationManager.AppSettings["CacheDirectory"];
			var feed = ConfigurationManager.AppSettings["SourceNugetFeed"];

			SourceNugetFeed = new Uri(feed);
			CacheDirectory = Path.IsPathRooted(directory)
					? directory
					: Path.Combine(HttpRuntime.AppDomainAppPath, directory);
		}
	}
}
