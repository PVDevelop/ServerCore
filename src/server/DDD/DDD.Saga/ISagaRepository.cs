namespace PVDevelop.UCoach.Saga
{
	public interface ISagaRepository
	{
		Saga GetSaga(SagaId id);

		void SaveSaga(Saga saga);
	}
}
