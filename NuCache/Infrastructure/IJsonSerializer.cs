namespace NuCache.Infrastructure
{
	public interface IJsonSerializer
	{
		string Serialize(object target);
		T Deserialize<T>(string json);
	}
}
