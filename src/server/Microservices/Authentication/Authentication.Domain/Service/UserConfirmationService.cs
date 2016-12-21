using System;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Domain.Service
{
	/// <summary>
	/// Сервис подтверждения пользователя.
	/// </summary>
	public class UserConfirmationService : IEventObserver<ISagaEvent>
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

		public void HandleEvent(ISagaEvent @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));
			When((dynamic)@event);
		}

		private void When(ConfirmUserRequested @event)
		{
			var confirmation = _confirmationRepository.GetConfirmation(@event.ConfirmationKey);

			confirmation.Confirm(@event.Id);

			_confirmationRepository.SaveConfirmation(confirmation);
		}

		private void When(ConfirmationApproved @event)
		{
			var user = _userRepository.GetUserById(@event.UserId);

			user.Confirm(@event.Id);

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
