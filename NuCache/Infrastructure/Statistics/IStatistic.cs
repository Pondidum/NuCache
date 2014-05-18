namespace NuCache.Infrastructure.Statistics
{
	public interface IStatistic<T>
	{
		string Name { get; }

		void Process(T input);
	}
}
