using System;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Application
{
	public class UserDao
	{
		private readonly ISagaProgressProvider _sagaProgressProvider;

		public UserDao(ISagaProgressProvider sagaProgressProvider)
		{
			if (sagaProgressProvider == null) throw new ArgumentNullException(nameof(sagaProgressProvider));
			_sagaProgressProvider = sagaProgressProvider;
		}

		public SagaStatus GetUserCreationResult(SagaId sagaId)
		{
			return _sagaProgressProvider.GetProgress(sagaId);
		}

		public SagaStatus GetUserConfirmationResult(SagaId sagaId)
		{
			return _sagaProgressProvider.GetProgress(sagaId);
		}
	}
}
