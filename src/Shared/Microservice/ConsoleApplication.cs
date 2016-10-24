using System;
using System.Threading;
using PVDevelop.UCoach.Logging;

namespace PVDevelop.UCoach.Microservice
{
	public class ConsoleApplication
	{
		private readonly CancellationTokenSource _cancellationTokenSource;
		private readonly IMicroservice _microservice;
		private readonly ILogger _logger;

		public ConsoleApplication(IMicroservice microservice)
		{
			if (microservice == null) throw new ArgumentNullException(nameof(microservice));

			_cancellationTokenSource = new CancellationTokenSource();
			_logger = LoggerHelper.GetLogger<ConsoleApplication>();
			_microservice = microservice;
		}

		public ConsoleApplication Start()
		{
			_logger.Info("Starting application");
			_logger.Info("Press Ctrl+C to exit");

			if (_microservice == null)
			{
				throw new InvalidOperationException("Not initialized");
			}

			Console.CancelKeyPress += (sender, ea) =>
			{
				_logger.Info("Stopping application...");

				Stop();

				_logger.Info("ConsoleApplication is stopped");
			};

			_microservice.Start(_cancellationTokenSource.Token);

			return this;
		}

		private void Stop()
		{
			_logger.Info("Stopping application");

			if (_microservice != null)
			{
				_cancellationTokenSource.Cancel();
				_microservice.Dispose();
			}

			_logger.Info("ConsoleApplication stopped");
		}
	}
}
