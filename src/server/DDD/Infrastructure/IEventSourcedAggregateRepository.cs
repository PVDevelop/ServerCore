using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Domain;

namespace PVDevelop.UCoach.Infrastructure
{
	/// <summary>
	/// Хранилище агрегатов, основанных на доменных событиях.
	/// </summary>
	public interface IEventSourcedAggregateRepository
	{
		/// <summary>
		/// Сохраняет последовательность событий, произошедших на агрегате в хранилище событий.
		/// </summary>
		/// <param name="aggregate">Сохраняемый агрегат.</param>
		void SaveAggregate(AEventSourcedAggregate aggregate);

		/// <summary>
		/// Восстанавливает агрегает по событиям из хранилища.
		/// </summary>
		/// <typeparam name="TAggregate">Тип восстанавливаемого агрегата.</typeparam>
		/// <param name="aggregateId">Идентификатор восстанавливаемого агрегата.</param>
		/// <param name="restoreAggregateCallback">Делегат восстановления агрегата.</param>
		/// <returns>Восстановленный агрегат.</returns>
		TAggregate RestoreAggregate<TAggregate>(
			Guid aggregateId,
			Func<Guid, int, IEnumerable<IDomainEvent>, TAggregate> restoreAggregateCallback)
			where TAggregate : AEventSourcedAggregate;
	}
}
