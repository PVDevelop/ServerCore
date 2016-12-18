using System;
using System.Collections.Generic;
using System.Threading;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Infrastructure.Port;

namespace PVDevelop.UCoach.Infrastructure.Adapter
{
	public class EventStoreObservable : IEventObservervable, IDisposable, IStartable
	{
		private readonly Dictionary<string, int> _observedStreams = new Dictionary<string, int>();
		private readonly List<IEventObserver> _observers = new List<IEventObserver>();
		private bool _disposed;
		private readonly IEventStore _eventStore;
		private readonly TimeSpan _pullingPeriod;
		private Timer _timer;

		public EventStoreObservable(IEventStore eventStore, TimeSpan pullingPeriod)
		{
			if (eventStore == null) throw new ArgumentNullException(nameof(eventStore));
			_eventStore = eventStore;
			_pullingPeriod = pullingPeriod;
		}

		public void AddObserver(IEventObserver observer)
		{
			if (observer == null) throw new ArgumentNullException(nameof(observer));
			_observers.Add(observer);
		}

		public void RemoveObserver(IEventObserver observer)
		{
			if (observer == null) throw new ArgumentNullException(nameof(observer));
			_observers.Remove(observer);
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
			var streams = _eventStore.GetAllStreams();
			foreach (var eventStream in streams)
			{
				int eventNumber;
				if (!_observedStreams.TryGetValue(eventStream.StreamId, out eventNumber))
				{
					eventNumber = 0;
				}

				var eventsData = eventStream.GetEvents(eventNumber, int.MaxValue);
				foreach (var @event in eventsData.Events)
				foreach (var eventObserver in _observers)
				{
					eventObserver.HandleEvent(eventStream.StreamId, @event);
				}

				_observedStreams[eventStream.StreamId] = eventsData.LatestVersion;
			}

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
	}
}
