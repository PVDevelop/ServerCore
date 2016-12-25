using System;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class AuthenticationContext : IDisposable
	{
		private bool _disposed;

		private readonly EventStorePuller _eventStorePuller;

		public IProcessManager ProcessManager { get; }

		public IUserRepository UserRepository { get; }

		public IConfirmationRepository ConfirmationRepository { get; }

		public IUserSessionRepository UserSessionRepository { get; }

		public AuthenticationContext(
			EventStorePuller eventStorePuller,
			IProcessManager processManager,
			IUserRepository userRepository,
			IConfirmationRepository confirmationRepository,
			IUserSessionRepository userSessionRepository)
		{
			if (eventStorePuller == null) throw new ArgumentNullException(nameof(eventStorePuller));
			if (processManager == null) throw new ArgumentNullException(nameof(processManager));

			_eventStorePuller = eventStorePuller;

			ProcessManager = processManager;

			UserRepository = userRepository;
			ConfirmationRepository = confirmationRepository;
			UserSessionRepository = userSessionRepository;
		}

		public void Start()
		{
			_eventStorePuller.Start();
		}

		public void Dispose()
		{
			if(_disposed) return;

			_eventStorePuller.Dispose();

			_disposed = true;
		}
	}
}
