using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Events
{
	public class ConfirmationApproved : ISagaEvent
	{
		public SagaId Id { get; }
		public SagaStatus Status => SagaStatus.Pending;
		public UserId UserId { get; }

		public ConfirmationApproved(
			SagaId sagaId, 
			UserId userId)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			Id = sagaId;
			UserId = userId;
		}
	}
}
