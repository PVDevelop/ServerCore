namespace PVDevelop.UCoach.Saga
{
	public interface ISagaProgressProvider
	{
		SagaStatus GetProgress(SagaId sagaId);
	}
}
