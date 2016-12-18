using System;

namespace PVDevelop.UCoach.Saga
{
	public class SagaMessageDispatchedEvent
	{
		public ISagaMessage SagaMessage { get; }

		public SagaMessageDispatchedEvent(ISagaMessage sagaMessage)
		{
			if (sagaMessage == null) throw new ArgumentNullException(nameof(sagaMessage));
			SagaMessage = sagaMessage;
		}
	}
}
