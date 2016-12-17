namespace PVDevelop.UCoach.Infrastructure.Port
{
	public interface IEventObserver
	{
		void HandleEvent(object @event);
	}
}
