using System;
using System.Collections.Generic;
using System.Linq;
using PVDevelop.UCoach.EventStore;

namespace PVDevelop.UCoach.Shared.EventSourcing
{
	public class EventSourcingRepository : IEventSourcingRepository
	{
		private readonly IEventStore _eventStore;

		public EventSourcingRepository(IEventStore eventStore)
		{
			if (eventStore == null) throw new ArgumentNullException(nameof(eventStore));

			_eventStore = eventStore;
		}

		public void SaveEventSourcing<TId, TEvent>(
			string streamIdPrefix,
			AEventSourcing<TId, TEvent> eventSourcing)
			where TEvent : class
		{
			var streamId = GetStreamId(streamIdPrefix, eventSourcing.Id);
			var stream = _eventStore.GetOrCreateStream(streamId);
			stream.SaveEvents(eventSourcing.Events);
		}

		public TEventSourcing RestoreEventSourcing<TId, TEvent, TEventSourcing>(
			string streamIdPrefix,
			TId id,
			Func<TId, int, IEnumerable<TEvent>, TEventSourcing> restoreAggregateCallback)
			where TEventSourcing : AEventSourcing<TId, TEvent>
			where TEvent : class
		{
			if (restoreAggregateCallback == null) throw new ArgumentNullException(nameof(restoreAggregateCallback));

			var streamId = GetStreamId(streamIdPrefix, id);
			var stream = _eventStore.GetStream(streamId);

			if (stream == null)
			{
				return null;
			}

			var eventsData = stream.GetEvents(0, int.MaxValue);

			var initialVersion = eventsData.LatestVersion;

			return restoreAggregateCallback(
				id,
				initialVersion,
				eventsData.Events.Cast<TEvent>());
		}

		private static string GetStreamId<TId>(string streamIdPrefix, TId id)
		{
			if (string.IsNullOrWhiteSpace(streamIdPrefix))
				throw new ArgumentException("Not set", nameof(streamIdPrefix));

			return $"{streamIdPrefix}.{id}";
		}
	}
}
