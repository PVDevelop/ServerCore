using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Domain;
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
			if (session == null) throw new ArgumentNullException(nameof(session));

			_eventSourcedAggregateRepository.SaveEventSourcing(StreamIdPrefix, session);
		}

		public IReadOnlyCollection<UserSession> GetSessions(UserId userId)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			return _eventSourcedAggregateRepository.RestoreAllEventSourcing<UserSessionId, IDomainEvent, UserSession>(
				StreamIdPrefix,
				(id, version, events) => new UserSession(id, version, events));
		}
	}
}
