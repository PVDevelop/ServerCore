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
			foreach (var eventObserver in _observers)
			{
				HandleEventForObserver(
					observer: eventObserver,
					@event: @event);
			}
			_observedStreams[eventStream.StreamId] = eventsData.LatestVersion;
		}

		private void HandleEventForObserver(
			object observer,
			object @event)
		{
			foreach (var implementedInterface in 
				observer.
					GetType().
					GetTypeInfo().
					ImplementedInterfaces)
			{
				var genericType = GetEventGenericTypeForObserver(implementedInterface, @event.GetType());

				if (genericType == null)
				{
					continue;
				}

				observer.
					GetType().
					GetRuntimeMethod("HandleEvent", new[] {genericType}).
					Invoke(observer, new[] {@event});
			}
		}

		private Type GetEventGenericTypeForObserver(Type observerType, Type eventType)
		{
			if (!observerType.IsConstructedGenericType ||
			    observerType.GetGenericTypeDefinition() != typeof(IEventObserver<>))
				return null;

			var genericType = observerType.GenericTypeArguments[0];
			if (eventType == genericType ||
			    eventType.GetTypeInfo().GetInterface(genericType.Name) != null ||
			    genericType.GetTypeInfo().IsSubclassOf(eventType))
			{
				return genericType;
			}

			return null;
		}
	}
}
