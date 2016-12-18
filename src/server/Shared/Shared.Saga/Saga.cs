using System;
using System.Collections.Generic;
using PVDevelop.UCoach.EventStore;

namespace PVDevelop.UCoach.Saga
{
	public class Saga : AEventSourcing<SagaId, ISagaMessage>
	{
		private readonly Dictionary<Type, ISagaMessage> _sagaMessages = 
			new Dictionary<Type, ISagaMessage>();

		public SagaStatus Status { get; private set; }

		public Saga(SagaId sagaId) : base(sagaId)
		{
		}

		public Saga(SagaId id, int initialVersion, IEnumerable<ISagaMessage> events)
			: base(id, initialVersion, events)
		{

		}

		protected override void When(ISagaMessage @event)
		{
			_sagaMessages.Add(@event.GetType(), @event);
			Status = @event.Status;
		}
	}
}
