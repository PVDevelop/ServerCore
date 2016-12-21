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
		/// Объект, выражающий состояние саги.
		/// </summary>
		object Progress { get; }
	}
}
