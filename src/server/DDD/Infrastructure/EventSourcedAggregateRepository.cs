using System;
using System.Linq;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.EventStore;

namespace PVDevelop.UCoach.Infrastructure
{
	/// <summary>
	/// Хранилище агрегатов, основанных на доменных событиях.
	/// </summary>
	public class EventSourcedAggregateRepository
	{
		private readonly IEventStore _eventStore;

		public EventSourcedAggregateRepository(IEventStore eventStore)
		{
			if (eventStore == null) throw new ArgumentNullException(nameof(eventStore));

			_eventStore = eventStore;
		}

		/// <summary>
		/// Сохраняет последовательность событий, произошедших на агрегате в хранилище событий.
		/// </summary>
		/// <param name="aggregate">Сохраняемый агрегат.</param>
		public void SaveAggregate(AEventSourcedAggregate aggregate)
		{
			var stream = _eventStore.CreateStream(aggregate.Id.ToString());
			stream.SaveEvents(aggregate.Events);
		}

		/// <summary>
		/// Восстанавливает агрегает по событиям из хранилища.
		/// </summary>
		/// <typeparam name="TAggregate">Тип восстанавливаемого агрегата.</typeparam>
		/// <param name="aggregateId">Идентификатор восстанавливаемого агрегата.</param>
		/// <returns>Восстановленный агрегат.</returns>
		public TAggregate RestoreAggregate<TAggregate>(Guid aggregateId)
			where TAggregate : AEventSourcedAggregate
		{
			var stream = _eventStore.GetStream(aggregateId.ToString());
			var events = stream.GetEvents().Cast<IDomainEvent>().ToArray();
			var initialVersion = events.Length;

			return (TAggregate) Activator.CreateInstance(
				typeof(TAggregate), 
				aggregateId, 
				initialVersion, 
				events);
		}
	}
}
