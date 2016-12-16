using System;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Infrastructure
{
	public class EventConsumerWithSagaRedirection : IEventConsumer
	{
		private readonly IEventStore _eventStore;
		private readonly ISagaMessageDispatcher _sagaMessageDispatcher;

		public EventConsumerWithSagaRedirection(IEventStore eventStore, ISagaMessageDispatcher sagaMessageDispatcher)
		{
			if (eventStore == null) throw new ArgumentNullException(nameof(eventStore));
			if (sagaMessageDispatcher == null) throw new ArgumentNullException(nameof(sagaMessageDispatcher));
			_eventStore = eventStore;
			_sagaMessageDispatcher = sagaMessageDispatcher;

			_eventStore.RegisterConsumer(this);
		}

		public void Consume(object @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));
			var sagaMessage = @event as ISagaMessage;
			if (sagaMessage == null) return;
			_sagaMessageDispatcher.Dispatch(sagaMessage);
		}
	}
}
