using System.Collections.Generic;
using NuCache.Infrastructure.NuGet;

namespace NuCache.Models
{
	public class StatisticsViewModel
	{
		public IEnumerable<KeyValuePair<PackageID, int>> DownloadCounts { get; set; }
	}
}
