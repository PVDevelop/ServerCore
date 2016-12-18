namespace PVDevelop.UCoach.EventStore
{
	public interface IEventObserver<in TEvent>
	{
		void HandleEvent(TEvent @event);
	}
}
