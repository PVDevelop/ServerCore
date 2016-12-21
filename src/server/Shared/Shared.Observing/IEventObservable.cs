namespace PVDevelop.UCoach.Shared.Observing
{
	public interface IEventObservable
	{
		void HandleEvent(string eventCategory, object @event);
	}
}
