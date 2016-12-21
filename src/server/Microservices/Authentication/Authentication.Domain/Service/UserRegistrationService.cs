using System;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Domain.Service
{
	/// <summary>
	/// Сервис регистрации пользователя.
	/// </summary>
	public class UserRegistrationService : 
		IEventObserver<CreateUserRequested>,
		IEventObserver<UserCreated>,
		IEventObserver<ConfirmationCreated>,
		IEventObserver<ConfirmationTransmittedToPending>
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfirmationRepository _confirmationRepository;
		private readonly IConfirmationKeyGenerator _confirmationKeyGenerator;
		private readonly IConfirmationSender _confirmationSender;

		public UserRegistrationService(
			IUserRepository userRepository,
			IConfirmationRepository confirmationRepository,
			IConfirmationKeyGenerator confirmationKeyGenerator,
			IConfirmationSender confirmationSender)
		{
			if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
			if (confirmationRepository == null) throw new ArgumentNullException(nameof(confirmationRepository));
			if (confirmationKeyGenerator == null) throw new ArgumentNullException(nameof(confirmationKeyGenerator));
			if (confirmationSender == null) throw new ArgumentNullException(nameof(confirmationSender));
			_userRepository = userRepository;
			_confirmationRepository = confirmationRepository;
			_confirmationKeyGenerator = confirmationKeyGenerator;
			_confirmationSender = confirmationSender;
		}

		public void HandleEvent(CreateUserRequested @event)
		{
			var user = new User(@event.UserId, @event.Email, @event.Password);
			_userRepository.SaveUser(user);
		}

		public void HandleEvent(UserCreated @event)
		{
			var confirmationKey = _confirmationKeyGenerator.Generate();

			var confirmation = new Confirmation(confirmationKey, @event.UserId);
			_confirmationRepository.SaveConfirmation(confirmation);
		}

		public void HandleEvent(ConfirmationCreated @event)
		{
			var confirmation = _confirmationRepository.GetConfirmation(@event.ConfirmationKey);

			confirmation.TransmitToPending();
			_confirmationRepository.SaveConfirmation(confirmation);
		}

		public void HandleEvent(ConfirmationTransmittedToPending @event)
		{
			_confirmationSender.Send(@event.ConfirmationKey);
		}
	}
}
