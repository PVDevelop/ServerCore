﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Infrastructure.Adapter;

namespace PVDevelop.UCoach.Infrastructure
{
	[TestFixture]
	public class EventSourcedAggregateRepositoryTests
	{
		[Test]
		public void SaveAndRestoreAggregate_InMemoryEventStore_RestoresExpectedAggregate()
		{
			var eventStore = new InMemoryEventStore();

			var aggregate = Aggregate.New(Guid.NewGuid());
			aggregate.Mutate(new QttyDomainEvent(55));

			var repository = new EventSourcingRepository(eventStore);
			repository.SaveEventSourcing(aggregate);
			var restoredAggregate = repository.RestoreEventSourcing<Guid, IDomainEvent, Aggregate>(aggregate.Id, Aggregate.Restore);

			Assert.NotNull(restoredAggregate.Events);
			Assert.AreEqual(aggregate.Id, restoredAggregate.Id);
			Assert.AreEqual(aggregate.Qtty, restoredAggregate.Qtty);
		}

		private class Aggregate : AEventSourcedAggregate<Guid>
		{
			public int Qtty { get; private set; }

			internal static Aggregate New(Guid id)
			{
				return new Aggregate(id);
			}

			internal static Aggregate Restore(Guid id, int version, IEnumerable<IDomainEvent> domainEvents)
			{
				return new Aggregate(id, version, domainEvents);
			}

			private Aggregate(Guid id) : base(id)
			{
			}

			private Aggregate(Guid id, int initialVersion, IEnumerable<IDomainEvent> domainEvents) :
				base(id, initialVersion, domainEvents)
			{
			}

			protected override void When(IDomainEvent @event)
			{
				When((dynamic) @event);
			}

			private void When(object @event)
			{
				throw new InvalidOperationException($"Event {@event} has not been processed.");
			}

			private void When(QttyDomainEvent @event)
			{
				Qtty = @event.Qtty;
			}
		}

		private class QttyDomainEvent : IDomainEvent
		{
			public int Qtty { get; }

			public QttyDomainEvent(int qtty)
			{
				Qtty = qtty;
			}
		}
	}
}