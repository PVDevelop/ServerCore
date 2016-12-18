using System;

namespace PVDevelop.UCoach.Saga
{
	public interface ISagaMessage
	{
		SagaId SagaId { get; }
		SagaStatus Status { get; }
	}
}
