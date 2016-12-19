using System;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Saga
{
	public class SagaMessageDispatcher : IEventObserver<ISagaMessage>
	{
		private readonly ISagaRepository _sagaRepository;

		public SagaMessageDispatcher(
			ISagaRepository sagaRepository)
		{
			if (sagaRepository == null) throw new ArgumentNullException(nameof(sagaRepository));
			_sagaRepository = sagaRepository;
		}

		public void HandleEvent(ISagaMessage message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			var saga = _sagaRepository.GetSaga(message.SagaId) ?? new Saga(message.SagaId);

			saga.Handle(message);
			_sagaRepository.SaveSaga(saga);
		}
	}
}
