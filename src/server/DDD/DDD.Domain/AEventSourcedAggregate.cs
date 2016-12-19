using System;
using System.Collections.Generic;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Domain
{
	/// <summary>
	/// Базовый класс агрегата, прелставленного цепочкой событий.
	/// </summary>
	public abstract class AEventSourcedAggregate<TId> : AEventSourcing<TId, IDomainEvent>
	{
		protected AEventSourcedAggregate(TId id) : base(id)
		{
		}

		protected AEventSourcedAggregate(
			TId id,
			int initialVersion,
			IEnumerable<IDomainEvent> events)
			: base(id, initialVersion, events)
		{
		}
	}
}
