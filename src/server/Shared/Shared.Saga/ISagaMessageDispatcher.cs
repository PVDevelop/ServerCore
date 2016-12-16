namespace PVDevelop.UCoach.Saga
{
	public interface ISagaMessageDispatcher
	{
		void Dispatch(ISagaMessage message);
	}
}
