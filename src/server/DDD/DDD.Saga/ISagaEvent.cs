using PVDevelop.UCoach.Domain;

namespace PVDevelop.UCoach.Saga
{
	public interface ISagaEvent : IDomainEvent
	{
		/// <summary>
		/// Идентификатор саги.
		/// </summary>
		SagaId Id { get; }

		/// <summary>
		/// Состояние саги.
		/// </summary>
		SagaStatus Status { get; }
	}
}
