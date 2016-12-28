using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Domain
{
	public class AggregateConstraint<TAggreateId> : AEventSourcing<AggregateConstraintId, IConstraintEvent>
	{
		public TAggreateId AggregateId { get; private set; }

		public AggregateConstraint(
			AggregateConstraintId id,
			TAggreateId aggregateId) 
			: base(id)
		{
			Mutate(new AggregateConstraintCreated<TAggreateId>(id, aggregateId));
		}

		public AggregateConstraint(
			AggregateConstraintId key, 
			int initialVersion, 
			IEnumerable<IConstraintEvent> events) 
			: base(key, initialVersion, events)
		{
		}

		protected override void When(IConstraintEvent @event)
		{
			HandleEvent((dynamic)@event);
		}

		private void HandleEvent(AggregateConstraintCreated<TAggreateId> @event)
		{
			AggregateId = @event.AggregateId;
		}

		private void HandleEvent(object @event)
		{
			throw new InvalidOperationException($"Unknown event '{@event}'.");
		}
	}
}
