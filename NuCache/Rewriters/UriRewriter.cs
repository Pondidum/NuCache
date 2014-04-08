using System;

namespace NuCache.Rewriters
{
	public class UriRewriter
	{
		public Uri TransformHost(Uri newHost, Uri input)
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
