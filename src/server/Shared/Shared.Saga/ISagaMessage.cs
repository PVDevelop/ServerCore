using System;

namespace PVDevelop.UCoach.Saga
{
	public interface ISagaMessage
	{
		Guid SagaId { get; }
	}
}
