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

		public Saga(SagaId id, int initialVersion, IEnumerable<ISagaMessage> messgaes)
			: base(id, initialVersion, messgaes)
		{

		}

		public void Handle(ISagaMessage message)
		{
			Mutate(message);
		}

		protected override void When(ISagaMessage message)
		{
			_sagaMessages.Add(message.GetType(), message);
			Status = message.Status;
		}
	}
}
