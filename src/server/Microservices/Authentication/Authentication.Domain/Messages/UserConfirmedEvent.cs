using System;
using PVDevelop.UCoach.Domain.SagaProgress;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class UserConfirmedEvent : ISagaEvent
	{
		public SagaId Id { get; }
		public object Progress { get; }

		public UserConfirmedEvent(
			SagaId sagaId,
			UserConfirmationProgress progress)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (progress == null) throw new ArgumentNullException(nameof(progress));
			Id = sagaId;
			Progress = progress;
		}
	}
}
