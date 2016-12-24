using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.Shared.EventSourcing
{
	/// <summary>
	/// Хранилище объектов, основанных на событиях.
	/// </summary>
	public interface IEventSourcingRepository
	{
		/// <summary>
		/// Сохраняет последовательность событий, произошедших на объекте в хранилище событий.
		/// </summary>
		/// <param name="streamIdPrefix">Префикс наименования стрима.</param>
		/// <param name="eventSourcing">Сохраняемый объект.</param>
		void SaveEventSourcing<TId, TEvent>(
			string streamIdPrefix,
			AEventSourcing<TId, TEvent> eventSourcing) where TEvent : class;

		/// <summary>
		/// Восстанавливает объект по событиям из хранилища.
		/// </summary>
		/// <typeparam name="TEventSourcing">Тип восстанавливаемого объекта.</typeparam>
		/// <typeparam name="TId">Тип идентификатора</typeparam>
		/// <typeparam name="TEvent">Тип (базовый) событий.</typeparam>
		/// <param name="streamIdPrefix">Префикс наименования стрима.</param>
		/// <param name="id">Идентификатор восстанавливаемого объекта.</param>
		/// <param name="restoreAggregateCallback">Делегат создания объекта.</param>
		/// <returns>Восстановленный объект.</returns>
		TEventSourcing RestoreEventSourcing<TId, TEvent, TEventSourcing>(
			string streamIdPrefix,
			TId id,
			Func<TId, int, IEnumerable<TEvent>, TEventSourcing> restoreAggregateCallback)
			where TEvent : class
			where TEventSourcing : AEventSourcing<TId, TEvent>;
	}
}
