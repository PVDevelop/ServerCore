namespace PVDevelop.UCoach.Shared.Observing
{
	public interface IEventObservable
	{
		void HandleEvent(object @event);
	}
}
