namespace PVDevelop.UCoach.EventStore
{
	public interface IEventObservervable
	{
		void AddObserver<TEvent>(IEventObserver<TEvent> observer);

		void RemoveObserver<TEvent>(IEventObserver<TEvent> observer);
	}
}
