namespace PVDevelop.UCoach.Shared.Observing
{
	public interface IEventObserver<in TEvent>
	{
		void HandleEvent(TEvent @event);
	}
}
