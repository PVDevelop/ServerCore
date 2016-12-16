namespace PVDevelop.UCoach.Saga
{
	public interface ISagaMessageConsumer
	{
		void Consume(ISagaMessage message);
	}
}