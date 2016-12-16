using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.EventStore
{
	public class InMemoryEventStore : IEventStore
	{
		private readonly Dictionary<string, InMemoryEventStream> _streams =
			new Dictionary<string, InMemoryEventStream>();

		private readonly List<IEventConsumer> _eventConsumers = new List<IEventConsumer>();

		public IEventStream GetOrCreateStream(string id)
		{
			InMemoryEventStream stream;
			if (!_streams.TryGetValue(id, out stream))
			{
				stream = new InMemoryEventStream(this);
				_streams.Add(id, stream);
			}

			return stream;
		}

		public IEventStream GetStream(string id)
		{
			return _streams[id];
		}

		public void RegisterConsumer(IEventConsumer consumer)
		{
			if (consumer == null) throw new ArgumentNullException(nameof(consumer));
			_eventConsumers.Add(consumer);
		}

		internal void NotifyNewEvent(object @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));

			foreach (var eventConsumer in _eventConsumers)
			{
				eventConsumer.Consume(@event);
			}
		}
	}
}
