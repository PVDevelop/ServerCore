using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.Saga
{
	public class InMemorySagaRepository : ISagaRepository
	{
		private readonly Dictionary<Guid, Saga> _sagas = new Dictionary<Guid, Saga>();

		public Saga GetSaga(Guid sagaId)
		{
			Saga saga;
			_sagas.TryGetValue(sagaId, out saga);
			return saga;
		}

		public void SaveSaga(Saga saga)
		{
			_sagas[saga.Id] = saga;
		}
	}
}
