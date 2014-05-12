using Newtonsoft.Json;

namespace NuCache.Infrastructure
{
	public class JsonSerializer : IJsonSerializer
	{
		public string Serialize(object target)
		{
			return JsonConvert.SerializeObject(target);
		}

		public T Deserialize<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}
