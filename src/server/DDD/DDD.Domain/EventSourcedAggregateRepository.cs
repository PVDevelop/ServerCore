using System;
using System.Collections.Generic;
using System.Linq;
using PVDevelop.UCoach.EventStore;

namespace PVDevelop.UCoach.Domain
{
	public class EventSourcedAggregateRepository : IEventSourcedAggregateRepository
	{
		private readonly IEventStore _eventStore;

		public EventSourcedAggregateRepository(IEventStore eventStore)
		{
			if (eventStore == null) throw new ArgumentNullException(nameof(eventStore));

			_eventStore = eventStore;
		}

		public void SaveAggregate<TId>(AEventSourcedAggregate<TId> aggregate)
		{
			var stream = _eventStore.GetOrCreateStream(aggregate.Id.ToString());
			stream.SaveEvents(aggregate.Events);
		}

		public TAggregate RestoreAggregate<TId, TAggregate>(
			TId aggregateId,
			Func<TId, int, IEnumerable<IDomainEvent>, TAggregate> restoreAggregateCallback)
			where TAggregate : AEventSourcedAggregate<TId>
		{
			if (restoreAggregateCallback == null) throw new ArgumentNullException(nameof(restoreAggregateCallback));
			var stream = _eventStore.GetStream(aggregateId.ToString());
			var eventsData = stream.GetEvents(0, int.MaxValue);

			var initialVersion = eventsData.LatestVersion;

			return restoreAggregateCallback(
				aggregateId,
				initialVersion,
				eventsData.Events.Cast<IDomainEvent>());
		}
	}
}
