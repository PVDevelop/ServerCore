using System;
using System.Collections.Generic;
using System.Linq;
using PVDevelop.UCoach.EventStore;

namespace PVDevelop.UCoach.Shared.EventSourcing
{
	public class EventSourcingRepository : IEventSourcingRepository
	{
		private readonly IEventStore _eventStore;

		public static string GetStreamPrefix(Type eventSourcingType)
		{
			return $"{eventSourcingType}.";
		}

		private static string GetStreamName<TId>(TId id, Type eventSourcingType)
		{
			return $"{GetStreamPrefix(eventSourcingType)}{id}";
		}

		public EventSourcingRepository(IEventStore eventStore)
		{
			if (eventStore == null) throw new ArgumentNullException(nameof(eventStore));

			_eventStore = eventStore;
		}

		public void SaveEventSourcing<TId, TEvent>(AEventSourcing<TId, TEvent> eventSourcing)
			where TEvent : class
		{
			var streamName = GetStreamName(eventSourcing.Id, eventSourcing.GetType());
			var stream = _eventStore.GetOrCreateStream(streamName);
			stream.SaveEvents(eventSourcing.Events);
		}

		public TEventSourcing RestoreEventSourcing<TId, TEvent, TEventSourcing>(
			TId eventSourcingId,
			Func<TId, int, IEnumerable<TEvent>, TEventSourcing> restoreAggregateCallback)
			where TEventSourcing : AEventSourcing<TId, TEvent>
			where TEvent : class
		{
			if (restoreAggregateCallback == null) throw new ArgumentNullException(nameof(restoreAggregateCallback));

			var streamName = GetStreamName(eventSourcingId, typeof(TEventSourcing));
			var stream = _eventStore.GetStream(streamName);

			if (stream == null)
			{
				return null;
			}

			var eventsData = stream.GetEvents(0, int.MaxValue);

			var initialVersion = eventsData.LatestVersion;

			return restoreAggregateCallback(
				eventSourcingId,
				initialVersion,
				eventsData.Events.Cast<TEvent>());
		}
	}
}
