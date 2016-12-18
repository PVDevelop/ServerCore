using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class ConfirmationTransmittedToPendingEvent : ISagaMessage, IDomainEvent
	{
		public SagaId SagaId { get; }
		public SagaStatus Status => SagaStatus.Succeeded;

		public ConfirmationKey ConfirmationKey { get; }

		public ConfirmationTransmittedToPendingEvent(SagaId sagaId, ConfirmationKey confirmationKey)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));
			SagaId = sagaId;
			ConfirmationKey = confirmationKey;
		}
	}
}
