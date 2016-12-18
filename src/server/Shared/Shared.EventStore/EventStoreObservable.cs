using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace PVDevelop.UCoach.EventStore
{
	public class EventStoreObservable : IEventObservervable, IDisposable
	{
		private readonly Dictionary<string, int> _observedStreams = new Dictionary<string, int>();
		private readonly List<object> _observers = new List<object>();
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

		public void AddObserver<TEvent>(IEventObserver<TEvent> observer)
		{
			if (observer == null) throw new ArgumentNullException(nameof(observer));
			_observers.Add(observer);
		}

		public void RemoveObserver<TEvent>(IEventObserver<TEvent> observer)
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
				foreach (var implementedInterface in eventObserver.GetType().GetTypeInfo().ImplementedInterfaces)
				{
					if (implementedInterface.IsConstructedGenericType &&
					    implementedInterface.GetGenericTypeDefinition() == typeof(IEventObserver<>))
					{
						var genericType = implementedInterface.GenericTypeArguments[0];
						if (@event.GetType() == genericType ||
						    @event.GetType().GetTypeInfo().GetInterface(genericType.Name) != null ||
						    genericType.GetTypeInfo().IsSubclassOf(@event.GetType()))
						{
							eventObserver.
								GetType().
								GetRuntimeMethod("HandleEvent", new[] {genericType}).
								Invoke(eventObserver, new[] {@event});
						}
					}

					_observedStreams[eventStream.StreamId] = eventsData.LatestVersion;
				}
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
