using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Events
{
	public class ConfirmationCreated : ISagaEvent
	{
		public SagaId Id { get; }
		public SagaStatus Status => SagaStatus.Pending;

		public ConfirmationKey ConfirmationKey { get; }
		public UserId UserId { get; }

		public ConfirmationCreated(
			SagaId sagaId, 
			ConfirmationKey confirmationKey, 
			UserId userId)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			Id = sagaId;
			UserId = userId;
			ConfirmationKey = confirmationKey;
		}
	}
}
