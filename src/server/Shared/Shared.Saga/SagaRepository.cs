using System;
using PVDevelop.UCoach.EventStore;

namespace PVDevelop.UCoach.Saga
{
	public class SagaRepository : ISagaRepository
	{
		private readonly IEventSourcingRepository _eventSourcingRepository;

		public SagaRepository(IEventSourcingRepository eventSourcingRepository)
		{
			if (eventSourcingRepository == null) throw new ArgumentNullException(nameof(eventSourcingRepository));
			_eventSourcingRepository = eventSourcingRepository;
		}

		public Saga GetSaga(SagaId id)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			return _eventSourcingRepository.RestoreEventSourcing<SagaId, ISagaMessage, Saga>(
				id,
				(sagaId, version, events) => new Saga(sagaId, version, events));
		}

		public void SaveSaga(Saga saga)
		{
			if (saga == null) throw new ArgumentNullException(nameof(saga));
			_eventSourcingRepository.SaveEventSourcing(saga);
		}
	}
}
