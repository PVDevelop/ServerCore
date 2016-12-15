using System;
using NLog;

namespace PVDevelop.UCoach.Logging
{
	internal class NLogAdapter : ILogger
	{
		private readonly Logger _logger;

		internal NLogAdapter(Logger logger)
		{
			if (logger == null) throw new ArgumentNullException(nameof(logger));
			_logger = logger;
		}

		public void Debug(Exception exception, string message)
		{
			_logger.Debug(exception, message);
		}

		public void Debug(string message)
		{
			_logger.Debug(message);
		}

		public void Info(Exception exception, string message)
		{
			_logger.Info(exception, message);
		}

		public void Info(string message)
		{
			_logger.Info(message);
		}

		public void Warning(Exception exception, string message)
		{
			_logger.Warn(exception, message);
		}

		public void Warning(string message)
		{
			_logger.Warn(message);
		}

		public void Error(Exception exception, string message)
		{
			_logger.Error(exception, message);
		}

		public void Error(string message)
		{
			_logger.Error(message);
		}

		public void Fatal(Exception exception, string message)
		{
			_logger.Fatal(exception, message);
		}

		public void Fatal(string message)
		{
			_logger.Fatal(message);
		}
	}
}
