using System;
using System.Collections.Generic;
using System.Reflection;

namespace PVDevelop.UCoach.Shared.Observing
{
	public class EventObservable : IEventObservable
	{
		private readonly List<object> _observers = new List<object>();

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

		public void HandleEvent(object @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));

			foreach (var eventObserver in _observers)
			{
				HandleEventForObserver(
					observer: eventObserver,
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
