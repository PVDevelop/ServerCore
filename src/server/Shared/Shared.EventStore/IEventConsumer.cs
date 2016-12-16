namespace PVDevelop.UCoach.EventStore
{
	public interface IEventConsumer
	{
		void Consume(object @event);
	}
}
