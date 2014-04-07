using System;

namespace NuCache
{
	public class UriHostTransformer
	{
		public Uri Transform(Uri newHost, Uri input)
		{
			var builder = new UriBuilder(input)
			{
				Scheme = newHost.Scheme,
				Host = newHost.Host,
				Port = newHost.IsDefaultPort ? -1 : newHost.Port,
			};

			return builder.Uri;
		}
	}
}
