namespace NuCache.Models
{
	public class ApplicationViewModel
	{
		public string Version { get; private set; }

		public ApplicationViewModel()
		{
			Version = GetType().Assembly.GetName().Version.ToString();
		}
	}
}
