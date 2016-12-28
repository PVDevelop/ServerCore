using System;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Shared.EventSourcing;
using PVDevelop.UCoach.Shared.Observing;
using PVDevelop.UCoach.Shared.ProcessManagement;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class AuthenticationContextBuilder
	{
		private readonly IEventStore _eventStore;
		private readonly IEventObservable _eventObservable;
		private readonly IProcessManager _processManager;
		private readonly IUserRepository _userRepository;
		private readonly IConfirmationRepository _confirmationRepository;
		private readonly IUserSessionRepository _userSessionRepository;

		public AuthenticationContextBuilder(IUtcTimeProvider utcTimeProvider)
		{
			_eventStore = new EventStore.EventStore();
			_eventObservable = new EventObservable();

			var eventSourcingRepository = new EventSourcingRepository(_eventStore);

			var constraintRepository = new AggregateConstraintRepository(eventSourcingRepository);

			_userRepository = new UserRepository(eventSourcingRepository, constraintRepository);
			_confirmationRepository = new ConfirmationRepository(eventSourcingRepository);
			_userSessionRepository = new UserSessionRepository(eventSourcingRepository);

			var processRepository = new ProcessRepository(eventSourcingRepository);

			var processCommandExecutor = new AuthProcessCommandExecutor(
				_userRepository,
				_confirmationRepository,
				_userSessionRepository,
				utcTimeProvider);

			var processCommandFactory = new AuthProcessCommandFactory();
			_processManager = new ProcessManager(processRepository, processCommandExecutor, processCommandFactory);

			_eventObservable.AddObserver(_processManager);
		}

		public AuthenticationContext Build()
		{
			return new AuthenticationContext(
				new EventStorePuller(_eventStore, _eventObservable, TimeSpan.FromMilliseconds(100)),
				_processManager,
				_userRepository, 
				_confirmationRepository, 
				_userSessionRepository);
		}
	}
}
