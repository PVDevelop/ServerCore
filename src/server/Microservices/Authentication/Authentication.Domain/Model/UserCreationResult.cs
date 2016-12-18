using System;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Model
{
	public class UserCreationResult
	{
		public SagaId SagaId { get; }
		public UserCreationState State { get; }

		public UserCreationResult(SagaId sagaId, UserCreationState state)
		{
			SagaId = sagaId;
			State = state;
		}
	}
}
