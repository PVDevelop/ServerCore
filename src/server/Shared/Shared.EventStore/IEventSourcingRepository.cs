using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.EventStore
{
	/// <summary>
	/// Хранилище объектов, основанных на событиях.
	/// </summary>
	public interface IEventSourcingRepository
	{
		/// <summary>
		/// Сохраняет последовательность событий, произошедших на объекте в хранилище событий.
		/// </summary>
		/// <param name="eventSourcing">Сохраняемый объект.</param>
		void SaveEventSourcing<TId, TEvent>(AEventSourcing<TId, TEvent> eventSourcing) where TEvent : class;

		/// <summary>
		/// Восстанавливает объект по событиям из хранилища.
		/// </summary>
		/// <typeparam name="TEventSourcing">Тип восстанавливаемого объекта.</typeparam>
		/// <typeparam name="TId">Тип идентификатора</typeparam>
		/// <typeparam name="TEvent">Тип (базовый) событий.</typeparam>
		/// <param name="eventSourcingId">Идентификатор восстанавливаемого объекта.</param>
		/// <param name="restoreAggregateCallback">Делегат создания объекта.</param>
		/// <returns>Восстановленный объект.</returns>
		TEventSourcing RestoreEventSourcing<TId, TEvent, TEventSourcing>(
			TId eventSourcingId,
			Func<TId, int, IEnumerable<TEvent>, TEventSourcing> restoreAggregateCallback)
			where TEventSourcing : AEventSourcing<TId, TEvent>
			where TEvent : class;
	}
}
