using System;

namespace PVDevelop.UCoach.Domain.Model
{
	public class UserCreationResult
	{
		public Guid SagaId { get; }
		public UserCreationState State { get; }

		public UserCreationResult(Guid sagaId, UserCreationState state)
		{
			SagaId = sagaId;
			State = state;
		}
	}
}
