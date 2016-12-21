using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Events
{
	public class ConfirmationTransmittedToPending : ISagaEvent
	{
		public SagaId Id { get; }
		public SagaStatus Status => SagaStatus.Success;

		public ConfirmationKey ConfirmationKey { get; }

		public ConfirmationTransmittedToPending(
			SagaId sagaId, 
			ConfirmationKey confirmationKey)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));
			Id = sagaId;
			ConfirmationKey = confirmationKey;
		}
	}
}
