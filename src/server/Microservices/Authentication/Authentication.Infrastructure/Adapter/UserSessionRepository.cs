using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class UserSessionRepository : IUserSessionRepository
	{
		public const string StreamIdPrefix = "Aggregate.UserSession";

		private readonly IEventSourcingRepository _eventSourcedAggregateRepository;

		public UserSessionRepository(IEventSourcingRepository eventSourcedAggregateRepository)
		{
			if (eventSourcedAggregateRepository == null)
				throw new ArgumentNullException(nameof(eventSourcedAggregateRepository));

			_eventSourcedAggregateRepository = eventSourcedAggregateRepository;
		}

		public void SaveSession(UserSession session)
		{
			_eventSourcedAggregateRepository.SaveEventSourcing(StreamIdPrefix, session);
		}
	}
}
