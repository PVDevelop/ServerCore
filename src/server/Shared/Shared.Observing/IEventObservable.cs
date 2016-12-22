namespace PVDevelop.UCoach.Shared.Observing
{
	public interface IEventObservable
	{
		void AddObserver(object observer);

		void RemoveObserver(object observer);

		void HandleEvent(string eventCategory, object @event);
	}
}
