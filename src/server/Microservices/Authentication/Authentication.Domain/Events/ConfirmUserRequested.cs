using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Events
{
	public class ConfirmUserRequested : ISagaEvent
	{
		public SagaId Id { get; }
		public SagaStatus Status => SagaStatus.Pending;

		public ConfirmationKey ConfirmationKey { get; }

		public ConfirmUserRequested(
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
