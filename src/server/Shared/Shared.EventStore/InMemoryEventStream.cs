using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.EventStore
{
	public class InMemoryEventStream : IEventStream
	{
		private readonly List<object> _events = new List<object>();

		public string StreamId { get; }

		public InMemoryEventStream(string streamId)
		{
			if (string.IsNullOrWhiteSpace(streamId)) throw new ArgumentException("streamId", nameof(streamId));
			StreamId = streamId;
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
		}

		public EventsData GetEvents(int startNumber, int endNumber)
		{
			var first = Math.Max(0, startNumber);
			var count = Math.Min(endNumber, _events.Count - first);
			var events = _events.GetRange(first, count).ToArray();
			return new EventsData(_events.Count, events);
		}
	}
}
