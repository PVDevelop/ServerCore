using System;

namespace PVDevelop.UCoach.Saga
{
	public class SagaMessageDispatcher : ISagaMessageDispatcher
	{
		private readonly ISagaMessageConsumer _sagaMessageConsumer;
		private readonly ISagaRepository _sagaRepository;

		public SagaMessageDispatcher(
			ISagaMessageConsumer sagaMessageConsumer,
			ISagaRepository sagaRepository)
		{
			if (sagaMessageConsumer == null) throw new ArgumentNullException(nameof(sagaMessageConsumer));
			if (sagaRepository == null) throw new ArgumentNullException(nameof(sagaRepository));
			_sagaMessageConsumer = sagaMessageConsumer;
			_sagaRepository = sagaRepository;
		}

		public void Dispatch(ISagaMessage message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			var saga = _sagaRepository.GetSaga(message.SagaId) ?? new Saga(message.SagaId);

			saga.Handle(message);
			_sagaRepository.SaveSaga(saga);

			_sagaMessageConsumer.Consume(message);
		}
	}
}
