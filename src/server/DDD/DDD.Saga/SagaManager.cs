using System;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Saga
{
	public class SagaManager : IEventObserver<ISagaEvent>, ISagaProgressProvider
	{
		private readonly ISagaRepository _sagaRepository;

		public SagaManager(
			ISagaRepository sagaRepository)
		{
			if (sagaRepository == null) throw new ArgumentNullException(nameof(sagaRepository));
			_sagaRepository = sagaRepository;
		}

		public void HandleEvent(ISagaEvent @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));

			var saga = _sagaRepository.GetSaga(@event.Id) ?? new Saga(@event.Id);

			saga.Handle(@event);
			_sagaRepository.SaveSaga(saga);
		}

		public object GetProgress(SagaId sagaId)
		{
			return _sagaRepository.GetSaga(sagaId).Progress;
		}
	}
}
