using System;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Domain.Service
{
	/// <summary>
	/// Сервис подтверждения пользователя.
	/// </summary>
	public class UserConfirmationService : 
		IEventObserver<UserConfirmed>,
		IEventObserver<ConfirmationApproved>
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserSessionRepository _userSessionRepository;

		public UserConfirmationService(
			IUserRepository userRepository,
			IUserSessionRepository userSessionRepository)
		{
			if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
			if (userSessionRepository == null) throw new ArgumentNullException(nameof(userSessionRepository));

			_userRepository = userRepository;
			_userSessionRepository = userSessionRepository;
		}

		public void HandleEvent(ConfirmationApproved @event)
		{
			var user = _userRepository.GetUserById(@event.UserId);

			user.Confirm();

			_userRepository.SaveUser(user);
		}

		public void HandleEvent(UserConfirmed @event)
		{
			var userSessionId = new UserSessionId(Guid.NewGuid());

			var session = new UserSession(userSessionId, @event.UserId);
			
			_userSessionRepository.SaveSession(session);
		}
	}
}
