using System;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class UserConfirmedEvent : ISagaMessage, IDomainEvent
	{
		public SagaId SagaId { get; }
		public SagaStatus Status => SagaStatus.Progress;

		public UserConfirmedEvent(SagaId sagaId)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			SagaId = sagaId;
		}
	}
}
