using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class ConfirmationCreatedEvent : ISagaMessage, IDomainEvent
	{
		public Guid SagaId { get; }
		public SagaStatus Status => SagaStatus.Progress;

		public ConfirmationKey ConfirmationKey { get; }
		public UserId UserId { get; }

		public ConfirmationCreatedEvent(Guid sagaId, ConfirmationKey confirmationKey, UserId userId)
		{
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			SagaId = sagaId;
			UserId = userId;
			ConfirmationKey = confirmationKey;
		}
	}
}
