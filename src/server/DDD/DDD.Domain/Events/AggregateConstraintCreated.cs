using System;

namespace PVDevelop.UCoach.Domain.Events
{
	public class AggregateConstraintCreated<TAggregateId> : IConstraintEvent
	{
		public AggregateConstraintId Id { get; }

		public TAggregateId AggregateId { get; }

		public AggregateConstraintCreated(
			AggregateConstraintId id,
			TAggregateId aggregateId)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			if (aggregateId == null) throw new ArgumentNullException(nameof(aggregateId));

			Id = id;
			AggregateId = aggregateId;
		}
	}
}
