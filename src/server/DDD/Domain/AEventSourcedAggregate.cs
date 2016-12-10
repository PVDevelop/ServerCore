using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.Domain
{
	/// <summary>
	/// Базовый класс агрегата, прелставленного цепочкой событий.
	/// </summary>
	public abstract class AEventSourcedAggregate
	{
		private readonly List<IDomainEvent> _events;

		/// <summary>
		/// Возвращает коллекцию событий, прозошедших при мутации агрегата после его восстановления.
		/// </summary>
		public IReadOnlyCollection<IDomainEvent> Events => _events;

		/// <summary>
		/// Идентификатор агрегата.
		/// </summary>
		public Guid Id { get; }

		/// <summary>
		/// Начальная версия агрегата в момент восстановления по событяим. Если не задана, то это новый агрегат.
		/// </summary>
		public int? InitialVersion { get; }

		/// <summary>
		/// Создание нового агрегата.
		/// </summary>
		/// <param name="creatingNew">
		/// Фейковый атрибут, добавленный для того, чтобы насленики не забывали опрелять все неоходимые конструкторы.
		/// Наследники всегда должны передавать true.
		/// </param>
		protected AEventSourcedAggregate(bool creatingNew)
		{
			if(!creatingNew) throw new InvalidOperationException("Only new aggregate can be created with this constructor");

			_events = new List<IDomainEvent>();
			Id = Guid.NewGuid();
		}

		/// <summary>
		/// Воссоздает агрегат по событиям.
		/// </summary>
		/// <param name="id">Идентификатор агрегата.</param>
		/// <param name="version">Текущая версия агрегата.</param>
		/// <param name="events">События, произошедшие на агрегате.</param>
		protected AEventSourcedAggregate(Guid id, int version, IEnumerable<IDomainEvent> events)
		{
			if (events == null) throw new ArgumentNullException(nameof(events));

			_events = new List<IDomainEvent>();
			Id = id;
			InitialVersion = version;
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
