using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model.UserSession;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class UserSessionHelper : IEventSourcingHelper<UserSessionId, IDomainEvent, UserSessionAggregate>
	{
		public string GetStreamName()
		{
			return "Aggregate.UserSession";
		}

		public UserSessionId ParseId(string id)
		{
			return new UserSessionId(Guid.Parse(id));
		}

		public string GetStringId(UserSessionId id)
		{
			return id.Value.ToString();
		}

		public UserSessionAggregate CreateEventSourcing(UserSessionId id, int version, IEnumerable<IDomainEvent> events)
		{
			return new UserSessionAggregate(id, version, events);
		}
	}
}
