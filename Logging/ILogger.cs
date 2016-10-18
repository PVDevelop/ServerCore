using System;

namespace PVDevelop.UCoach.Logging
{
	public interface ILogger
	{
		void Debug(Exception exception, string message);

		void Debug(string message);

		void Info(Exception exception, string message);

		void Info(string message);

		void Warning(Exception exception, string message);

		void Warning(string message);

		void Error(Exception exception, string message);

		void Error(string message);

		void Fatal(Exception exception, string message);

		void Fatal(string message);
	}
}
