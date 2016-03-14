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
		public string LogDirectory { get; set; }

		public Configuration()
		{
			var cache = ConfigurationManager.AppSettings["CacheDirectory"];
			var log = ConfigurationManager.AppSettings["LogDirectory"];
			var feed = ConfigurationManager.AppSettings["SourceNugetFeed"];

			SourceNugetFeed = new Uri(feed);
			CacheDirectory = Path.IsPathRooted(cache)
				? cache
				: Path.Combine(HttpRuntime.AppDomainAppPath, cache);
			LogDirectory = Path.IsPathRooted(log)
				? log
				: Path.Combine(HttpRuntime.AppDomainAppPath, log);

		}
	}
}
