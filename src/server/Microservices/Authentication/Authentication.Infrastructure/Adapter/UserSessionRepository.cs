using System;
using System.Collections.Generic;
using System.Linq;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Domain.Model.UserSession;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class UserSessionRepository : IUserSessionRepository
	{
		private readonly IEventSourcingRepository _eventSourcedAggregateRepository;

		public UserSessionRepository(IEventSourcingRepository eventSourcedAggregateRepository)
		{
			if (eventSourcedAggregateRepository == null)
				throw new ArgumentNullException(nameof(eventSourcedAggregateRepository));

			_eventSourcedAggregateRepository = eventSourcedAggregateRepository;
		}

		public void SaveSession(UserSessionAggregate session)
		{
			if (session == null) throw new ArgumentNullException(nameof(session));

			_eventSourcedAggregateRepository.SaveEventSourcing<
				UserSessionHelper,
				UserSessionId,
				IDomainEvent,
				UserSessionAggregate>(session);
		}

		public IReadOnlyCollection<UserSessionAggregate> GetSessions(UserId userId)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			var allSessions = _eventSourcedAggregateRepository.RestoreAllEventSourcings<
				UserSessionHelper,
				UserSessionId,
				IDomainEvent,
				UserSessionAggregate>();

			return allSessions.Where(s=>s.UserId == userId).ToArray();
		}
	}
}
