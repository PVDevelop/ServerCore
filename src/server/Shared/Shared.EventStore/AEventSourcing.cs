using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.EventStore
{
	/// <summary>
	/// Базовый класс агрегата, прелставленного цепочкой событий.
	/// </summary>
	public abstract class AEventSourcing<TId, TEvent>
	{
		private readonly List<TEvent> _events;

		/// <summary>
		/// Возвращает коллекцию событий, прозошедших при мутации объекта после его восстановления.
		/// </summary>
		public IReadOnlyCollection<TEvent> Events => _events;

		/// <summary>
		/// Идентификатор объекта.
		/// </summary>
		public TId Id { get; }

		/// <summary>
		/// Версия объекта в момент восстановления по событяим. Если не задана, то это новый объекта.
		/// </summary>
		public int? InitialVersion { get; }

		/// <summary>
		/// Создание нового объекта.
		/// </summary>
		/// <param name="id">Идентификатор объекта.</param>
		protected AEventSourcing(TId id)
		{
			if (Equals(id, default(TId))) throw new ArgumentException("Not set", nameof(id));

			_events = new List<TEvent>();
			Id = id;
		}

		/// <summary>
		/// Воссоздает объект по событиям.
		/// </summary>
		/// <param name="id">Идентификатор объекта.</param>
		/// <param name="initialVersion">Начальная версия объекта.</param>
		/// <param name="events">События, произошедшие в объекте с момента его создания.</param>
		protected AEventSourcing(TId id, int initialVersion, IEnumerable<TEvent> events)
		{
			if (Equals(id, default(TId))) throw new ArgumentException("Not set", nameof(id));
			if (events == null) throw new ArgumentNullException(nameof(events));

			_events = new List<TEvent>();
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
		public void Mutate(TEvent @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));

			When(@event);

			_events.Add(@event);
		}

		protected abstract void When(TEvent @event);
	}
}
