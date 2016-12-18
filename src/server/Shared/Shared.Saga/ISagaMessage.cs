using System;

namespace PVDevelop.UCoach.Saga
{
	public interface ISagaMessage
	{
		Guid SagaId { get; }
		SagaStatus Status { get; }
	}
}
