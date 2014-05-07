﻿using System.Web.Http;
using NuCache.Infrastructure;

namespace NuCache
{
	public static class ConfigureErrorHandling
	{
		public static void Register(HttpConfiguration config)
		{
			config.MessageHandlers.Add(new LoggingMessageHandler());
			config.Filters.Add(new ElmahErrorHandler());
		}
	}
}
