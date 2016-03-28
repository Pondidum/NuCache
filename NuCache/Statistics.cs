using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NuCache.Infrastructure;

namespace NuCache
{
	public class Statistics
	{
		private readonly IFileSystem _fileSystem;
		private readonly IConfiguration _config;
		private List<Dto> _stats;

		public Statistics(IFileSystem fileSystem, IConfiguration config)
		{
			_fileSystem = fileSystem;
			_config = config;
			_stats = new List<Dto>();
		}

		public void Add(string packageName, string host)
		{
			var dto = new Dto
			{
				Package = PackageName.Parse(packageName),
				Timestamp = DateTime.UtcNow,
				Host = host
			};

			_stats.Add(dto);
			_fileSystem.AppendToFile(_config.StatsFile, JsonConvert.SerializeObject(dto));
		}

		public async void LoadAsync()
		{
			if (_fileSystem.FileExists(_config.StatsFile) == false)
				return;

			await Task.Run(() =>
			{
				var lines = _fileSystem
					.ReadFileLines(_config.StatsFile)
					.Select(JsonConvert.DeserializeObject<Dto>)
					.ToList();

				_stats = lines;
			});
		}

		public IEnumerable<PackageAggregation> ForAll()
		{
			return _stats
				.GroupBy(s => s.Package.ToString(), StringComparer.OrdinalIgnoreCase)
				.Select(g => new PackageAggregation
				{
					Name = g.First().Package.Name,
					Version = g.First().Package.Version,
					Added = g.Min(e => e.Timestamp),
					Downloads = g.Count()
				});
		}

		public class PackageAggregation
		{
			public string Name { get; set; }
			public string Version { get; set; }
			public int Downloads { get; set; }
			public DateTime Added { get; set; }
		}

		private class Dto
		{
			public PackageName Package { get; set; }
			public DateTime Timestamp { get; set; }
			public string Host { get; set; }
		}
	}
}
