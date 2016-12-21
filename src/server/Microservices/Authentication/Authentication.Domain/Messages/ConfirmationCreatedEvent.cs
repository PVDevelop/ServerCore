using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.SagaProgress;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class ConfirmationCreatedEvent : ISagaEvent
	{
		public SagaId Id { get; }
		public object Progress { get; }

		public ConfirmationKey ConfirmationKey { get; }
		public UserId UserId { get; }

		public ConfirmationCreatedEvent(
			SagaId sagaId, 
			UserCreationProgress progress,
			ConfirmationKey confirmationKey, 
			UserId userId)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (progress == null) throw new ArgumentNullException(nameof(progress));
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			Id = sagaId;
			Progress = progress;
			UserId = userId;
			ConfirmationKey = confirmationKey;
		}
	}
}
