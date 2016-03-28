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
		private readonly Configuration _config;
		private List<Dto> _stats;

		public Statistics(IFileSystem fileSystem, Configuration config)
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

		private class Dto
		{
			public PackageName Package { get; set; }
			public DateTime Timestamp { get; set; }
			public string Host { get; set; }
		}
	}
}

