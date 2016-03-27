using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NuCache
{
	public class Statistics
	{
		private readonly Configuration _config;
		private List<Dto> _stats;

		public Statistics(Configuration config)
		{
			_config = config;
			_stats = new List<Dto>();
		}

		public void Add(string packageName, string host)
		{
			var dto = new Dto { Package = packageName, Timestamp = DateTime.UtcNow, Host = host };

			_stats.Add(dto);

			File.AppendAllLines(_config.StatsFile, new[] { JsonConvert.SerializeObject(dto) });
		}

		public async void LoadAsync()
		{
			if (File.Exists(_config.StatsFile) == false)
				return;

			await Task.Run(() =>
			{
				var lines = File
					.ReadAllLines(_config.StatsFile)
					.Select(JsonConvert.DeserializeObject<Dto>)
					.ToList();

				_stats = lines;
			});
		}

		private class Dto
		{
			public string Package { get; set; }
			public DateTime Timestamp { get; set; }
			public string Host { get; set; }
		}
	}
}
