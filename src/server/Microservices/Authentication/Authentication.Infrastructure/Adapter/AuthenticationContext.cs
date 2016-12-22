using System;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.EventStore;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class AuthenticationContext : IDisposable
	{
		private bool _disposed;

		private readonly EventStorePuller _eventStorePuller;

		public IUserRepository UserRepository { get; private set; }

		public IConfirmationRepository ConfirmationRepository { get; private set; }

		public IUserSessionRepository UserSessionRepository { get; private set; }

		public AuthenticationContext(
			EventStorePuller eventStorePuller,
			IUserRepository userRepository,
			IConfirmationRepository confirmationRepository,
			IUserSessionRepository userSessionRepository)
		{
			if (eventStorePuller == null) throw new ArgumentNullException(nameof(eventStorePuller));

			_eventStorePuller = eventStorePuller;

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
