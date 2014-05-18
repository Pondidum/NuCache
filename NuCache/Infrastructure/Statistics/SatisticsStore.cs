using System;
using System.IO;
using System.Threading.Tasks;

namespace NuCache.Infrastructure.Statistics
{
	public class SatisticsStore
	{
		private readonly IFileSystem _fileSystem;
		private readonly ApplicationSettings _settings;
		private readonly IJsonSerializer _serializer;

		public SatisticsStore(IFileSystem fileSystem, ApplicationSettings settings, IJsonSerializer serializer)
		{
			_fileSystem = fileSystem;
			_settings = settings;
			_serializer = serializer;
		}

		public void BeginLoadFromDisk(Action<HttpStatistic> read)
		{
			Task.Run(() =>
			{
				if (_fileSystem.FileExists(_settings.StatisticsPath) == false)
				{
					return;
				}

				using (var reader = new StreamReader(_fileSystem.ReadFile(_settings.StatisticsPath)))
				{
					var line = "";

					while ((line = reader.ReadLine()) != null)
					{
						read(_serializer.Deserialize<HttpStatistic>(line));

					}

				}

			});
		}

		public void Append(HttpStatistic container)
		{
			var json = _serializer.Serialize(container);

			using (var ms = new MemoryStream())
			{
				using (var sw = new StreamWriter(ms))
				{
					sw.WriteLine(json);
					sw.Flush();

					ms.Position = 0;

					_fileSystem.AppendFile(_settings.StatisticsPath, ms);

				}
			}
		}
	}
}