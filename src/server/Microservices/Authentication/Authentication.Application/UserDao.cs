using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Application
{
	public class UserDao
	{
		private readonly ISagaRepository _sagaRepository;

		public UserDao(ISagaRepository sagaRepository)
		{
			if (sagaRepository == null) throw new ArgumentNullException(nameof(sagaRepository));
			_sagaRepository = sagaRepository;
		}

		public UserCreationResult GetUserCreationResult(SagaId sagaId)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));

			var saga = _sagaRepository.GetSaga(sagaId);
			if (saga != null && saga.Status == SagaStatus.Succeeded)
			{
				return new UserCreationResult(sagaId, UserCreationState.Succeeded);
			}
			return new UserCreationResult(sagaId, UserCreationState.Pending);
		}
	}
}
