using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Infrastructure
{
	public interface ISagaEventStoreMessagePublisher
	{
		void Publish(ISagaMessage message);
	}
}
