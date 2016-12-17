using System;
using System.Collections.Concurrent;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;

namespace PVDevelop.UCoach.Domain.Service
{
	public class InMemoryUserProcessRepository : IUserProcessRepository
	{
		private readonly ConcurrentDictionary<Guid, UserCreationResult> _userCreationResults = 
			new ConcurrentDictionary<Guid, UserCreationResult>();

		public UserCreationResult GetUserCreationResult(Guid sagaId)
		{
			UserCreationResult userCreationResult;
			_userCreationResults.TryGetValue(sagaId, out userCreationResult);
			return userCreationResult;
		}

		public void SetUserCreationResult(UserCreationResult userCreationResult)
		{
			if (userCreationResult == null) throw new ArgumentNullException(nameof(userCreationResult));
			_userCreationResults[userCreationResult.SagaId] = userCreationResult;
		}
	}
}
