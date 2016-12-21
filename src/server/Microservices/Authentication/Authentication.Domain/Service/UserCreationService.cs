using System;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Domain.Service
{
	/// <summary>
	/// Сервис создания пользователя.
	/// </summary>
	public class UserCreationService : IEventObserver<ISagaEvent>
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfirmationRepository _confirmationRepository;
		private readonly IConfirmationKeyGenerator _confirmationKeyGenerator;
		private readonly IConfirmationSender _confirmationSender;

		public UserCreationService(
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

		public void HandleEvent(ISagaEvent @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));
			When((dynamic)@event);
		}

		private void When(CreateUserRequested @event)
		{
			var user = new User(@event.Id, new UserId(Guid.NewGuid()), @event.Email, @event.Password);
			_userRepository.SaveUser(user);
		}

		private void When(UserCreated userCreatedEvent)
		{
			var confirmationKey = _confirmationKeyGenerator.Generate();

			var confirmation = new Confirmation(userCreatedEvent.Id, confirmationKey, userCreatedEvent.UserId);
			_confirmationRepository.SaveConfirmation(confirmation);
		}

		private void When(ConfirmationCreated confirmationCreatedEvent)
		{
			_confirmationSender.Send(confirmationCreatedEvent.ConfirmationKey);

			var confirmation = _confirmationRepository.GetConfirmation(confirmationCreatedEvent.ConfirmationKey);

			confirmation.TransmitToPending(confirmationCreatedEvent.Id);
			_confirmationRepository.SaveConfirmation(confirmation);
		}

		private void When(object message)
		{
		}
	}
}
