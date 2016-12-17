using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;

namespace PVDevelop.UCoach.Application
{
	public class UserDao
	{
		private readonly IUserProcessRepository _userProcessRepository;

		public UserDao(IUserProcessRepository userProcessRepository)
		{
			if (userProcessRepository == null) throw new ArgumentNullException(nameof(userProcessRepository));
			_userProcessRepository = userProcessRepository;
		}

		public UserCreationResult GetUserCreationResult(Guid sagaId)
		{
			return _userProcessRepository.GetUserCreationResult(sagaId);
		}
	}
}
