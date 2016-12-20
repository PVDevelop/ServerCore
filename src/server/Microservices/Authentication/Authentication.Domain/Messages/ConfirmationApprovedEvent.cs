using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class ConfirmationApprovedEvent : ISagaMessage, IDomainEvent
	{
		public SagaId SagaId { get; }
		public SagaStatus Status => SagaStatus.Progress;
		public UserId UserId { get; }

		public ConfirmationApprovedEvent(SagaId sagaId, UserId userId)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			SagaId = sagaId;
			UserId = userId;
		}
	}
}
