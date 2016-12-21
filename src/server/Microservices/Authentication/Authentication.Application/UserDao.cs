using System;
using PVDevelop.UCoach.Domain.SagaProgress;
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

		public UserCreationProgress GetUserCreationResult(SagaId sagaId)
		{
			return (UserCreationProgress) _sagaProgressProvider.GetProgress(sagaId);
		}

		public UserConfirmationProgress GetUserConfirmationResult(SagaId sagaId)
		{
			return (UserConfirmationProgress) _sagaProgressProvider.GetProgress(sagaId);
		}
	}
}
