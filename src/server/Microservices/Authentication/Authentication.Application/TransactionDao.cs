using System;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Application
{
	public class TransactionDao
	{
		private readonly ISagaRepository _sagaRepository;

		public TransactionDao(ISagaRepository sagaRepository)
		{
			if (sagaRepository == null) throw new ArgumentNullException(nameof(sagaRepository));
			_sagaRepository = sagaRepository;
		}

		public TransactionStatus GetStatus(Guid transactionId)
		{
			var sagaId = new SagaId(transactionId);
			var saga = _sagaRepository.GetSaga(sagaId);
			if (saga == null)
			{
				return TransactionStatus.Unknown;
			}

			switch (saga.Status)
			{
				case SagaStatus.New:
				case SagaStatus.Progress:
					return TransactionStatus.Pending;
				case SagaStatus.Succeeded:
					return TransactionStatus.Succeeded;
				default:
					throw new IndexOutOfRangeException();
			}
		}
	}
}
