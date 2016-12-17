namespace PVDevelop.UCoach.Infrastructure.Port
{
	public interface IEventObservervable
	{
		void AddObserver(IEventObserver observer);

		void RemoveObserver(IEventObserver observer);
	}
}
