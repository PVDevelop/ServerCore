using System.Collections.Generic;

namespace PVDevelop.UCoach.Shared.EventSourcing
{
	/// <summary>
	/// Хранилище объектов, основанных на событиях.
	/// </summary>
	public interface IEventSourcingRepository
	{
		void SaveEventSourcing<THelper, TId, TEvent, TEventSourcing>(TEventSourcing eventSourcing)
			where THelper : IEventSourcingHelper<TId, TEvent, TEventSourcing>, new()
			where TEventSourcing : AEventSourcing<TId, TEvent>;

		TEventSourcing RestoreEventSourcing<THelper, TId, TEvent, TEventSourcing>(
			TId eventSourcingId)
			where THelper : IEventSourcingHelper<TId, TEvent, TEventSourcing>, new()
			where TEventSourcing : AEventSourcing<TId, TEvent>;

		IReadOnlyCollection<TEventSourcing> RestoreAllEventSourcings<THelper, TId, TEvent, TEventSourcing>()
			where THelper : IEventSourcingHelper<TId, TEvent, TEventSourcing>, new()
			where TEventSourcing : AEventSourcing<TId, TEvent>;
	}
}
