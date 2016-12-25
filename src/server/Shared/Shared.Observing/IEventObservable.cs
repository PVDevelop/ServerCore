namespace PVDevelop.UCoach.Shared.Observing
{
	public interface IEventObservable
	{
		void AddObserver<TObserver>(IEventObserver<TObserver> observer);

		void HandleEvent(object @event);
	}
}
