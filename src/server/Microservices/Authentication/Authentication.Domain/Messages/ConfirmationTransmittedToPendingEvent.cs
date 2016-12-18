using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class ConfirmationTransmittedToPendingEvent : ISagaMessage, IDomainEvent
	{
		public Guid SagaId { get; }
		public SagaStatus Status => SagaStatus.Succeeded;

		public ConfirmationKey ConfirmationKey { get; }

		public ConfirmationTransmittedToPendingEvent(Guid sagaId, ConfirmationKey confirmationKey)
		{
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));
			SagaId = sagaId;
			ConfirmationKey = confirmationKey;
		}
	}
}
