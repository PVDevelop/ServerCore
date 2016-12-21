using System;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Saga
{
	public class SagaRepository : ISagaRepository
	{
		private readonly IEventSourcingRepository _eventSourcingRepository;
		private readonly string _streamIdPrefix;

		public SagaRepository(IEventSourcingRepository eventSourcingRepository, string streamIdPrefix)
		{
			if (eventSourcingRepository == null) throw new ArgumentNullException(nameof(eventSourcingRepository));
			if(string.IsNullOrWhiteSpace(streamIdPrefix))
				throw new ArgumentException("Not set", nameof(streamIdPrefix));

			_eventSourcingRepository = eventSourcingRepository;
			_streamIdPrefix = streamIdPrefix;
		}

		public Saga GetSaga(SagaId id)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			return _eventSourcingRepository.RestoreEventSourcing<SagaId, ISagaEvent, Saga>(
				_streamIdPrefix,
				id,
				(sagaId, version, events) => new Saga(sagaId, version, events));
		}

		public void SaveSaga(Saga saga)
		{
			if (saga == null) throw new ArgumentNullException(nameof(saga));
			_eventSourcingRepository.SaveEventSourcing(_streamIdPrefix, saga);
		}
	}
}
