using System.Collections.Generic;
using NuCache.Infrastructure.NuGet;

namespace NuCache.Infrastructure.Statistics.Processors
{
	public class PackageDownloads : IStatistic<HttpStatistic>
	{
		public string Name { get { return "Packages Downloaded"; } }

		private readonly Dictionary<PackageID, int> _results;

		public PackageDownloads()
		{
			_results = new Dictionary<PackageID, int>();
		}

		public void Process(HttpStatistic input)
		{
			var id = new PackageID(input.PackageName, input.PackageVersion);

			if (_results.ContainsKey(id) == false)
			{
				_results[id] = 0;
			}

			_results[id]++;
		}
	}
}
