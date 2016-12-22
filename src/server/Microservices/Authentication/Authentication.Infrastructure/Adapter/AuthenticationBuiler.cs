using System;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Shared.EventSourcing;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class AuthenticationContextBuiler
	{
		private readonly IEventStore _eventStore;
		private readonly IEventObservable _eventObservable;
		private readonly IEventSourcingRepository _eventSourcingRepository;
		private IUserRepository _userRepository;
		private IConfirmationRepository _confirmationRepository;
		private IUserSessionRepository _userSessionRepository;

		public AuthenticationContextBuiler()
		{
			_eventStore = new EventStore.EventStore();
			_eventObservable = new EventObservable();
			_eventSourcingRepository = new EventSourcingRepository(_eventStore);
		}

		public AuthenticationContextBuiler WithUserRepository()
		{
			_userRepository = new UserRepository(_eventSourcingRepository);
			return this;
		}

		public AuthenticationContextBuiler WithConfirmationRepository()
		{
			_confirmationRepository = new ConfirmationRepository(_eventSourcingRepository);
			return this;
		}

		public AuthenticationContextBuiler WithUserSessionRepository()
		{
			_userSessionRepository = new UserSessionRepository(_eventSourcingRepository);
			return this;
		}

		public AuthenticationContextBuiler WithUserRegistrationService(
			IConfirmationSender confirmationSender)
		{
			var service = new UserRegistrationService(
				_userRepository,
				_confirmationRepository,
				new ConfirmationKeyGenerator(),
				confirmationSender);

			_eventObservable.AddObserver(service);
			
			return this;
		}

		public AuthenticationContextBuiler WithUserConfirmationService()
		{
			var service = new UserConfirmationService(
				_userRepository,
				_userSessionRepository);

			_eventObservable.AddObserver(service);

			return this;
		}

		public AuthenticationContextBuiler WithEventObserver(object eventObserver)
		{
			_eventObservable.AddObserver(eventObserver);

			return this;
		}

		public AuthenticationContext Build()
		{
			return new AuthenticationContext(
				new EventStorePuller(_eventStore, _eventObservable, TimeSpan.FromMilliseconds(100)),
				_userRepository, 
				_confirmationRepository, 
				_userSessionRepository);
		}
	}
}
