using System;
using System.Collections.Generic;
using NUnit.Framework;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.EventStore;

namespace PVDevelop.UCoach.Infrastructure
{
	[TestFixture]
	public class EventSourcedAggregateRepositoryTests
	{
		[Test]
		public void SaveAndRestoreAggregate_InMemoryEventStore_RestoresExpectedAggregate()
		{
			var eventStore = new InMemoryEventStore();

			var aggregate = new Aggregate();
			aggregate.Mutate(new QttyDomainEvent(55));

			var repository = new EventSourcedAggregateRepository(eventStore);
			repository.SaveAggregate(aggregate);
			var restoredAggregate = repository.RestoreAggregate<Aggregate>(aggregate.Id);

			Assert.NotNull(restoredAggregate.Events);
			Assert.AreEqual(aggregate.Id, restoredAggregate.Id);
			Assert.AreEqual(aggregate.Qtty, restoredAggregate.Qtty);
		}

		private class Aggregate : AEventSourcedAggregate
		{
			public int Qtty { get; private set; }

			public Aggregate() : base(true)
			{
			}

			public Aggregate(Guid id, int version, IEnumerable<IDomainEvent> domainEvents) :
				base(id, version, domainEvents)
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

			private void When(QttyDomainEvent domainEvent)
			{
				Qtty = domainEvent.Qtty;
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