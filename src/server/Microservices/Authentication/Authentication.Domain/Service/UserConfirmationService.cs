using System;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Domain.Service
{
	/// <summary>
	/// Сервис подтверждения пользователя.
	/// </summary>
	public class UserConfirmationService : IEventObserver<IDomainEvent>
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfirmationRepository _confirmationRepository;
		private readonly IUserSessionRepository _userSessionRepository;

		public UserConfirmationService(
			IUserRepository userRepository,
			IConfirmationRepository confirmationRepository,
			IUserSessionRepository userSessionRepository)
		{
			if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
			if (confirmationRepository == null) throw new ArgumentNullException(nameof(confirmationRepository));
			if (userSessionRepository == null) throw new ArgumentNullException(nameof(userSessionRepository));
			_userRepository = userRepository;
			_confirmationRepository = confirmationRepository;
			_userSessionRepository = userSessionRepository;
		}

		public void ConfirmUser(ConfirmationKey confirmationKey)
		{
			var confirmation = _confirmationRepository.GetConfirmation(confirmationKey);

			confirmation.Confirm();

			_confirmationRepository.SaveConfirmation(confirmation);
		}

		public void HandleEvent(IDomainEvent @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));
			When((dynamic)@event);
		}

		private void When(ConfirmationApproved @event)
		{
			var user = _userRepository.GetUserById(@event.UserId);

			user.Confirm();

			_userRepository.SaveUser(user);
		}

		private void When(UserConfirmed @event)
		{
			throw new NotImplementedException();
		}

		private void When(object @event)
		{
		}
	}
}
