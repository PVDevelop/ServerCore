using System.Collections.Generic;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model.Confirmation;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class ConfirmationHelper : IEventSourcingHelper<ConfirmationKey, IDomainEvent, ConfirmationAggregate>
	{
		public string GetStreamName()
		{
			return "Aggregate.Confirmation";
		}

		public ConfirmationKey ParseId(string id)
		{
			return new ConfirmationKey(id);
		}

		public string GetStringId(ConfirmationKey id)
		{
			return id.Value;
		}

		public ConfirmationAggregate CreateEventSourcing(ConfirmationKey id, int version, IEnumerable<IDomainEvent> events)
		{
			return new ConfirmationAggregate(id, version, events);
		}
	}
}