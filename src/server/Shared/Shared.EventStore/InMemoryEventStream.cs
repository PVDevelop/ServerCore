using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.EventStore
{
	public class InMemoryEventStream : IEventStream
	{
		private readonly InMemoryEventStore _ownerEventStore;
		private readonly List<object> _events = new List<object>();

		internal InMemoryEventStream(InMemoryEventStore ownerEventStore)
		{
			if (ownerEventStore == null) throw new ArgumentNullException(nameof(ownerEventStore));
			_ownerEventStore = ownerEventStore;
		}

		public void SaveEvents(IReadOnlyCollection<object> events)
		{
			if (events == null) throw new ArgumentNullException(nameof(events));

			foreach (var @event in events)
			{
				if (@event == null)
				{
					throw new ArgumentException("One of the events is null.");
				}
			}

			_events.AddRange(events);

			foreach (var @event in _events)
			{
				_ownerEventStore.NotifyNewEvent(@event);
			}
		}

		public IEnumerable<object> GetEvents()
		{
			return _events;
		}
	}
}
