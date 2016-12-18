using System;
using PVDevelop.UCoach.Infrastructure.Port;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Infrastructure.Adapter
{
	public class EventConsumerWithSagaRedirection : IEventObserver
	{
		private readonly ISagaMessageDispatcher _sagaMessageDispatcher;
		private readonly EventStoreFilter _filter;

		public EventConsumerWithSagaRedirection(
			ISagaMessageDispatcher sagaMessageDispatcher,
			EventStoreFilter filter)
		{
			if (sagaMessageDispatcher == null) throw new ArgumentNullException(nameof(sagaMessageDispatcher));

			_sagaMessageDispatcher = sagaMessageDispatcher;
			_filter = filter;
		}

		public void HandleEvent(string streamId, object @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));

			var sagaMessage = @event as ISagaMessage;

			if (sagaMessage == null) return;

			if (!_filter.Filtrate(streamId)) return;

			_sagaMessageDispatcher.Dispatch(sagaMessage);
		}
	}
}
