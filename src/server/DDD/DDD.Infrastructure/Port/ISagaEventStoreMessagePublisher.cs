using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Infrastructure.Port
{
	public interface ISagaEventStoreMessagePublisher
	{
		void Publish(ISagaMessage message);
	}
}
