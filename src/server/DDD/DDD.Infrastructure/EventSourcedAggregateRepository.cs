using System;
using System.Collections.Generic;
using System.Linq;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.EventStore;

namespace PVDevelop.UCoach.Infrastructure
{
	public class EventSourcedAggregateRepository : IEventSourcedAggregateRepository
	{
		private readonly IEventStore _eventStore;

		public EventSourcedAggregateRepository(IEventStore eventStore)
		{
			if (eventStore == null) throw new ArgumentNullException(nameof(eventStore));

			_eventStore = eventStore;
		}

		public void SaveAggregate(AEventSourcedAggregate aggregate)
		{
			var stream = _eventStore.GetOrCreateStream(aggregate.Id.ToString());
			stream.SaveEvents(aggregate.Events);
		}

		public TAggregate RestoreAggregate<TAggregate>(
			Guid aggregateId, 
			Func<Guid, int, IEnumerable<IDomainEvent>, TAggregate> restoreAggregateCallback)
			where TAggregate : AEventSourcedAggregate
		{
			if (restoreAggregateCallback == null) throw new ArgumentNullException(nameof(restoreAggregateCallback));
			var stream = _eventStore.GetStream(aggregateId.ToString());
			var events = stream.GetEvents().Cast<IDomainEvent>().ToArray();
			var initialVersion = events.Length;

			return restoreAggregateCallback(
				aggregateId, 
				initialVersion, 
				events);
		}
	}
}
