using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.SagaProgress;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class ConfirmUserMessage : ISagaEvent
	{
		public SagaId Id { get; }
		public object Progress { get; }

		public ConfirmationKey ConfirmationKey { get; }

		public ConfirmUserMessage(
			SagaId sagaId, 
			UserConfirmationProgress progress, 
			ConfirmationKey confirmationKey)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (progress == null) throw new ArgumentNullException(nameof(progress));
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));

			Id = sagaId;
			Progress = progress;
			ConfirmationKey = confirmationKey;
		}
	}
}
