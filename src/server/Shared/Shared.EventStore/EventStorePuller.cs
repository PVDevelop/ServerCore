using System;
using System.Collections.Generic;
using System.Threading;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.EventStore
{
	public class EventStorePuller : IDisposable
	{
		private readonly Dictionary<string, int> _observedStreams = new Dictionary<string, int>();
		private bool _disposed;
		private readonly IEventStore _eventStore;
		private readonly IEventObservable _observable;
		private readonly TimeSpan _pullingPeriod;
		private Timer _timer;

		public EventStorePuller(
			IEventStore eventStore,
			IEventObservable observable,
			TimeSpan pullingPeriod)
		{
			if (eventStore == null) throw new ArgumentNullException(nameof(eventStore));
			if (observable == null) throw new ArgumentNullException(nameof(observable));
			_eventStore = eventStore;
			_observable = observable;
			_pullingPeriod = pullingPeriod;
		}

		public void Dispose()
		{
			if (_disposed) return;

			if (_timer != null)
			{
				_timer.Dispose();
				_timer = null;
			}

			_disposed = true;
		}

		public void Start()
		{
			if (_disposed) throw new ObjectDisposedException(GetType().Name);
			if (_timer != null) throw new InvalidOperationException("Already started.");

			_timer = new Timer(Tick, null, _pullingPeriod, Timeout.InfiniteTimeSpan);
		}

		private void Tick(object state)
		{
			HandleEvents();

			try
			{
				if (!_disposed)
				{
					_timer.Change(_pullingPeriod, Timeout.InfiniteTimeSpan);
				}
			}
			catch (ObjectDisposedException)
			{
			}
		}

		private void HandleEvents()
		{
			var streams = _eventStore.GetAllStreams();
			foreach (var eventStream in streams)
			{
				HandleEventsForEventStream(eventStream);
			}
		}

		private void HandleEventsForEventStream(IEventStream eventStream)
		{
			int eventNumber;
			if (!_observedStreams.TryGetValue(eventStream.StreamId, out eventNumber))
			{
				eventNumber = 0;
			}

			var eventsData = eventStream.GetEvents(eventNumber, int.MaxValue);
			foreach (var @event in eventsData.Events)
			{
				_observable.HandleEvent(@event);
			}

			_observedStreams[eventStream.StreamId] = eventsData.LatestVersion;
		}
	}
}