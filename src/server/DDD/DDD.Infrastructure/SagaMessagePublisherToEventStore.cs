using System;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Infrastructure
{
	public class SagaMessagePublisherToEventStore : ISagaEventStoreMessagePublisher
	{
		private readonly IEventStore _eventStore;

		public SagaMessagePublisherToEventStore(IEventStore eventStore)
		{
			if (eventStore == null) throw new ArgumentNullException(nameof(eventStore));
			_eventStore = eventStore;
		}

		public void Publish(ISagaMessage message)
		{
			var stream = _eventStore.GetOrCreateStream(message.GetType().Name);
			stream.SaveEvents(new[] {message});
		}
	}
}
