using System;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace PVDevelop.UCoach.Logging
{
	public static class LoggerHelper
	{
		public static ILogger GetLogger<T>()
		{
			return new NLogAdapter(LogManager.GetLogger(typeof(T).Name));
		}

		public static void UseLogger(ILoggerFactory loggerFactory)
		{
			if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));

			loggerFactory.AddNLog();
		}
	}
}
