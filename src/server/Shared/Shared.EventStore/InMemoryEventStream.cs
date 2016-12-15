using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.EventStore
{
	public class InMemoryEventStream : IEventStream
	{
		private readonly List<object> _events = new List<object>();

		public void SaveEvents(IEnumerable<object> events)
		{
			if (events == null) throw new ArgumentNullException(nameof(events));
			_events.AddRange(events);
		}

		public IEnumerable<object> GetEvents()
		{
			return _events;
		}
	}
}
