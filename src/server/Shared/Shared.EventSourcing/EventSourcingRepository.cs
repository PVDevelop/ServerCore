using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
			where TId : IEventSourcingIdentifier, new()
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
			where TId : IEventSourcingIdentifier, new()
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

		public IReadOnlyCollection<TEventSourcing> RestoreAllEventSourcing<TId, TEvent, TEventSourcing>(
			string streamIdPrefix,
			Func<TId, int, IEnumerable<TEvent>, TEventSourcing> restoreAggregateCallback)
			where TEvent : class
			where TEventSourcing : AEventSourcing<TId, TEvent>
			where TId : IEventSourcingIdentifier, new()
		{
			var regex = new Regex(streamIdPrefix);
			return
				_eventStore.
					GetStreams(regex).
					Select(stream =>
					{
						var eventsData = stream.GetEvents(0, int.MaxValue);

						var initialVersion = eventsData.LatestVersion;

						var id = GetEventSourcingId<TId>(stream.StreamId, streamIdPrefix);
						return restoreAggregateCallback(
							id,
							initialVersion,
							eventsData.Events.Cast<TEvent>());
					}).
					ToArray();
		}

		private static string GetStreamId<TId>(string streamName, TId eventSourcingId)
			where TId : IEventSourcingIdentifier
		{
			return $"{streamName}.{eventSourcingId.GetIdString()}";
		}

		private static TId GetEventSourcingId<TId>(string streamId, string streamName)
			where TId : IEventSourcingIdentifier, new()
		{
			var idString = streamId.Substring(streamName.Length + 1);

			var eventSourcingId = new TId();
			eventSourcingId.ParseId(idString);

			return eventSourcingId;
		}
	}
}
