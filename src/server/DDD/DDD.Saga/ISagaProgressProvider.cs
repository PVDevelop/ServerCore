namespace PVDevelop.UCoach.Saga
{
	public interface ISagaProgressProvider
	{
		object GetProgress(SagaId sagaId);
	}
}
