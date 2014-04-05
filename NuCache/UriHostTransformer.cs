using System;

namespace NuCache
{
	public class UriHostTransformer
	{
		private readonly Uri _targetHost;

		public UriHostTransformer(Uri targetHost)
		{
			_targetHost = targetHost;
		}

		public Uri Transform(Uri input)
		{
			var builder = new UriBuilder(input)
			{
				Scheme = _targetHost.Scheme,
				Host = _targetHost.Host,
				Port = -1
			};

			return builder.Uri;
		}
	}
}
