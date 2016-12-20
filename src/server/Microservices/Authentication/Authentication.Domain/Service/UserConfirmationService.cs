using System;
using PVDevelop.UCoach.Domain.Messages;
using PVDevelop.UCoach.Saga;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Domain.Service
{
	/// <summary>
	/// Сервис подтверждения пользователя.
	/// </summary>
	public class UserConfirmationService : IEventObserver<SagaMessageDispatchedEvent>
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfirmationRepository _confirmationRepository;

		public UserConfirmationService(
			IUserRepository userRepository,
			IConfirmationRepository confirmationRepository)
		{
			if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
			if (confirmationRepository == null) throw new ArgumentNullException(nameof(confirmationRepository));
			_userRepository = userRepository;
			_confirmationRepository = confirmationRepository;
		}

		public void HandleEvent(SagaMessageDispatchedEvent @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));
			When((dynamic)@event.SagaMessage);
		}

		private void When(ConfirmUserMessage @event)
		{
			var confirmation = _confirmationRepository.GetConfirmation(@event.ConfirmationKey);

			confirmation.Confirm(@event.SagaId);

			_confirmationRepository.SaveConfirmation(confirmation);
		}

		private void When(ConfirmationApprovedEvent @event)
		{
			var user = _userRepository.GetUserById(@event.UserId);

			user.Confirm(@event.SagaId);

			_userRepository.SaveUser(user);
		}

		private void When(object @event)
		{
		}
	}
}
