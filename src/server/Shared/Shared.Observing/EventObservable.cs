using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PVDevelop.UCoach.Shared.Observing
{
	public class EventObservable : IEventObservable
	{
		private readonly List<Tuple<ObservableFilter, object>> _observers = 
			new List<Tuple<ObservableFilter, object>>();

		public void AddObserver<TEvent>(ObservableFilter filter, IEventObserver<TEvent> observer)
		{
			if (filter == null) throw new ArgumentNullException(nameof(filter));
			if (observer == null) throw new ArgumentNullException(nameof(observer));
			_observers.Add(new Tuple<ObservableFilter, object>(filter, observer));
		}

		public void RemoveObserver<TEvent>(IEventObserver<TEvent> observer)
		{
			if (observer == null) throw new ArgumentNullException(nameof(observer));
			var item = _observers.Single(t=>t.Item2 == observer);
			_observers.Remove(item);
		}

		public void HandleEvent(string eventCategory, object @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));

			foreach (var tuple in _observers.Where(t=>t.Item1.Match(eventCategory)))
			{
				HandleEventForObserver(
					observer: tuple.Item2,
					@event: @event);
			}
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
