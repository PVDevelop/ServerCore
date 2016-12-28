using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class UserHelper : IEventSourcingHelper<UserId, IDomainEvent, UserAggregate>
	{
		public string GetStreamName()
		{
			return "Aggregate.User";
		}

		public UserId ParseId(string id)
		{
			return new UserId(Guid.Parse(id));
		}

		public string GetStringId(UserId id)
		{
			return id.Value.ToString();
		}

		public UserAggregate CreateEventSourcing(UserId id, int version, IEnumerable<IDomainEvent> events)
		{
			return new UserAggregate(id, version, events);
		}
	}
}
