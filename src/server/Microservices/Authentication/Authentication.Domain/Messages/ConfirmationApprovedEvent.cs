using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.SagaProgress;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class ConfirmationApprovedEvent : ISagaEvent
	{
		public SagaId Id { get; }
		public object Progress { get; }
		public UserId UserId { get; }

		public ConfirmationApprovedEvent(
			SagaId sagaId, 
			UserCreationProgress progress,
			UserId userId)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (progress == null) throw new ArgumentNullException(nameof(progress));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			Id = sagaId;
			Progress = progress;
			UserId = userId;
		}
	}
}
