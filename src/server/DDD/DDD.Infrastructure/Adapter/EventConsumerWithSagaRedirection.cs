using System;
using PVDevelop.UCoach.Infrastructure.Port;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Infrastructure.Adapter
{
	public class EventConsumerWithSagaRedirection : IEventObserver
	{
		private readonly ISagaMessageDispatcher _sagaMessageDispatcher;

		public EventConsumerWithSagaRedirection(ISagaMessageDispatcher sagaMessageDispatcher)
		{
			if (sagaMessageDispatcher == null) throw new ArgumentNullException(nameof(sagaMessageDispatcher));

			_sagaMessageDispatcher = sagaMessageDispatcher;
		}

		public void HandleEvent(object @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));
			var sagaMessage = @event as ISagaMessage;
			if (sagaMessage == null) return;
			_sagaMessageDispatcher.Dispatch(sagaMessage);
		}
	}
}
