namespace PVDevelop.UCoach.Infrastructure.Port
{
	public interface IEventObserver
	{
		void HandleEvent(string steamId, object @event);
	}
}
