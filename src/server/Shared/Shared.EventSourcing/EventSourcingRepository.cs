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

		public void SaveEventSourcing<THelper, TId, TEvent, TEventSourcing>(TEventSourcing eventSourcing)
			where THelper : IEventSourcingHelper<TId, TEvent, TEventSourcing>, new()
			where TEventSourcing : AEventSourcing<TId, TEvent>
		{
			if (eventSourcing == null) throw new ArgumentNullException(nameof(eventSourcing));

			var helper = new THelper();

			var streamId = GetStreamId(
				streamName: helper.GetStreamName(),
				eventSourcingId: helper.GetStringId(eventSourcing.Id));

			var stream = _eventStore.GetOrCreateStream(streamId);

			stream.SaveEvents(eventSourcing.Events.Cast<object>().ToArray());
		}

		public TEventSourcing RestoreEventSourcing<THelper, TId, TEvent, TEventSourcing>(TId eventSourcingId)
			where THelper : IEventSourcingHelper<TId, TEvent, TEventSourcing>, new()
			where TEventSourcing : AEventSourcing<TId, TEvent>
		{
			var helper = new THelper();

			var streamId = GetStreamId(
				streamName: helper.GetStreamName(),
				eventSourcingId: helper.GetStringId(eventSourcingId));

			var stream = _eventStore.GetStream(streamId);

			if (stream == null)
			{
				return null;
			}

			var eventsData = stream.GetEvents(0, int.MaxValue);

			var initialVersion = eventsData.LatestVersion;

			return helper.CreateEventSourcing(
				eventSourcingId,
				initialVersion,
				eventsData.Events.Cast<TEvent>());
		}

		public IReadOnlyCollection<TEventSourcing> RestoreAllEventSourcings<THelper, TId, TEvent, TEventSourcing>()
			where THelper : IEventSourcingHelper<TId, TEvent, TEventSourcing>, new()
			where TEventSourcing : AEventSourcing<TId, TEvent>
		{
			var helper = new THelper();

			var streamName = helper.GetStreamName();
			var regex = new Regex(streamName);

			return
				_eventStore.
					GetStreams(regex).
					Select(stream =>
					{
						var eventsData = stream.GetEvents(0, int.MaxValue);

						var initialVersion = eventsData.LatestVersion;

						var id = GetEventSourcingId(
							streamId: stream.StreamId, 
							streamName: streamName);

						var eventSourcingId = helper.ParseId(id);

						return helper.CreateEventSourcing(
							eventSourcingId, 
							initialVersion, 
							eventsData.Events.Cast<TEvent>());
					}).
					ToArray();
		}

		private static string GetEventSourcingId(string streamId, string streamName)
		{
			var idString = streamId.Substring(streamName.Length + 1);
			return idString;
		}

		private static string GetStreamId(string streamName, string eventSourcingId)
		{
			return $"{streamName}.{eventSourcingId}";
		}
	}
}
