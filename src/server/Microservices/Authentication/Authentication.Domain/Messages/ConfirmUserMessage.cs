using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class ConfirmUserMessage : ISagaMessage
	{
		public SagaId SagaId { get; }
		public SagaStatus Status => SagaStatus.New;

		public ConfirmationKey ConfirmationKey { get; }

		public ConfirmUserMessage(SagaId sagaId, ConfirmationKey confirmationKey)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));

			SagaId = sagaId;
			ConfirmationKey = confirmationKey;
		}
	}
}
