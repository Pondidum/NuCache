using System;

namespace NuCache
{
	public interface IConfiguration
	{
		Uri SourceNugetFeed { get; set; }
		string CacheDirectory { get; set; }
		string LogDirectory { get; set; }
		string StatsFile { get; set; }
	}
}
