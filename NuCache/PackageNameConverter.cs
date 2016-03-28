using System;
using Newtonsoft.Json;

namespace NuCache
{
	public class PackageNameConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(PackageName);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue(value.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return PackageName.Parse(Convert.ToString(reader.Value));
		}
	}
}
