using System;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Events
{
	public class UserConfirmed : ISagaEvent
	{
		public SagaId Id { get; }
		public SagaStatus Status => SagaStatus.Pending;

		public UserConfirmed(
			SagaId sagaId)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));

			Id = sagaId;
		}
	}
}
