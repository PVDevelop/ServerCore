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
			AEventSourcing<TId, TEvent> eventSourcing)
			where TEvent : class
			where TId : IEventSourcingIdentifier, new();

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
			where TEventSourcing : AEventSourcing<TId, TEvent>
			where TId : IEventSourcingIdentifier, new();

		/// <summary>
		/// Восстанавлиает все оъекты определенного типа.
		/// </summary>
		/// <typeparam name="TId">Тип идентификатора объекта.</typeparam>
		/// <typeparam name="TEvent">Тип событий.</typeparam>
		/// <typeparam name="TEventSourcing">Тип объекта.</typeparam>
		/// <param name="streamIdPrefix">Имя стрима, из которого восстанавлиаются обекты.</param>
		/// <param name="restoreAggregateCallback">Делегат инстанцирования объекта.</param>
		/// <returns>Коллекция восстановленных оъектов.</returns>
		IReadOnlyCollection<TEventSourcing> RestoreAllEventSourcing<TId, TEvent, TEventSourcing>(
			string streamIdPrefix,
			Func<TId, int, IEnumerable<TEvent>, TEventSourcing> restoreAggregateCallback)
			where TEvent : class
			where TEventSourcing : AEventSourcing<TId, TEvent>
			where TId : IEventSourcingIdentifier, new();
	}
}
