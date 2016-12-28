using System.Collections.Generic;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Domain
{
	public class AggregateConstraintHelper<TAggregateId> : IEventSourcingHelper<AggregateConstraintId, IConstraintEvent, AggregateConstraint<TAggregateId>>
	{
		public string GetStreamName()
		{
			return "Aggregate.Constraint";
		}

		public AggregateConstraintId ParseId(string id)
		{
			var splittedId = id.Split('#');
			return new AggregateConstraintId(splittedId[0], splittedId[1]);
		}

		public string GetStringId(AggregateConstraintId id)
		{
			return $"{id.Key}#{id.Value}";
		}

		public AggregateConstraint<TAggregateId> CreateEventSourcing(AggregateConstraintId id, int version, IEnumerable<IConstraintEvent> events)
		{
			return new AggregateConstraint<TAggregateId>(id, version, events);
		}
	}
}
