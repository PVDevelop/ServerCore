using System;

namespace PVDevelop.UCoach.Saga
{
	public interface ISagaRepository
	{
		Saga GetSaga(Guid sagaId);

		void SaveSaga(Saga saga);
	}
}
