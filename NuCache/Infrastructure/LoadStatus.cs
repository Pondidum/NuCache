namespace NuCache.Infrastructure
{
	public class LoadStatus<T>
	{
		public bool Success { get; private set; }
		public T Target { get; private set; }

		public LoadStatus(T target, bool success)
		{
			Target = target;
			Success = success;
		}
	}
}
