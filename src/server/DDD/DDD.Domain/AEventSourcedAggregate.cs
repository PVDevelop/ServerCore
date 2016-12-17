using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.Domain
{
	/// <summary>
	/// Базовый класс агрегата, прелставленного цепочкой событий.
	/// </summary>
	public abstract class AEventSourcedAggregate<TId>
	{
		private readonly List<IDomainEvent> _events;

		/// <summary>
		/// Возвращает коллекцию событий, прозошедших при мутации агрегата после его восстановления.
		/// </summary>
		public IReadOnlyCollection<IDomainEvent> Events => _events;

		/// <summary>
		/// Идентификатор агрегата.
		/// </summary>
		public TId Id { get; }

		/// <summary>
		/// Версия агрегата в момент восстановления по событяим. Если не задана, то это новый агрегат.
		/// </summary>
		public int? InitialVersion { get; }

		/// <summary>
		/// Создание нового агрегата.
		/// </summary>
		/// <param name="id">Идентификатор агрегата</param>
		protected AEventSourcedAggregate(TId id)
		{
			if (Equals(id, default(TId))) throw new ArgumentException("Not set", nameof(id));

			_events = new List<IDomainEvent>();
			Id = id;
		}

		/// <summary>
		/// Воссоздает агрегат по событиям.
		/// </summary>
		/// <param name="id">Идентификатор агрегата.</param>
		/// <param name="initialVersion">Текущая версия агрегата.</param>
		/// <param name="events">События, произошедшие на агрегате.</param>
		protected AEventSourcedAggregate(TId id, int initialVersion, IEnumerable<IDomainEvent> events)
		{
			if (Equals(id, default(TId))) throw new ArgumentException("Not set", nameof(id));
			if (events == null) throw new ArgumentNullException(nameof(events));

			_events = new List<IDomainEvent>();
			Id = id;
			InitialVersion = initialVersion;

			foreach (var @event in events)
			{
				When(@event);
			}
		}

		/// <summary>
		/// Мутировать состояние агрегата при помощи доменного события
		/// </summary>
		public void Mutate(IDomainEvent @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));

			When(@event);

			_events.Add(@event);
		}

		protected abstract void When(IDomainEvent @event);
	}
}
