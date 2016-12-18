using System;
using System.Collections.Generic;
using PVDevelop.UCoach.EventStore;

namespace PVDevelop.UCoach.Saga
{
	public class Saga : AEventSourcing<SagaId, SagaMessageDispatchedEvent>
	{
		private readonly Dictionary<Type, ISagaMessage> _sagaMessages =
			new Dictionary<Type, ISagaMessage>();

		public SagaStatus Status { get; private set; }

		public Saga(SagaId sagaId) : base(sagaId)
		{
		}

		public Saga(SagaId id, int initialVersion, IEnumerable<SagaMessageDispatchedEvent> messgaes)
			: base(id, initialVersion, messgaes)
		{

		}

		public void Handle(ISagaMessage message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			var @event = new SagaMessageDispatchedEvent(message);
			Mutate(@event);
		}

		protected override void When(SagaMessageDispatchedEvent @event)
		{
			_sagaMessages.Add(@event.SagaMessage.GetType(), @event.SagaMessage);
			Status = @event.SagaMessage.Status;
		}
	}
}
