using System;
using PVDevelop.UCoach.Domain.Messages;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Domain.Service
{
	/// <summary>
	/// Сервис создания пользователя.
	/// </summary>
	public class UserCreationService : IEventObserver<SagaMessageDispatchedEvent>
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

		public void HandleEvent(SagaMessageDispatchedEvent @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));

			When((dynamic)@event.SagaMessage);
		}

		private void When(CreateUserMessage createUserMessage)
		{
			var userCreated = new UserCreatedEvent(
				createUserMessage.SagaId,
				new UserId(createUserMessage.SagaId.Value),
				createUserMessage.Email,
				createUserMessage.Password);

			var user = new User(userCreated);
			_userRepository.AddUser(user);
		}

		private void When(UserCreatedEvent userCreatedEvent)
		{
			var confirmationKey = _confirmationKeyGenerator.Generate();

			var confirmationCreated = new ConfirmationCreatedEvent(
				userCreatedEvent.SagaId,
				confirmationKey, 
				userCreatedEvent.UserId);

			var confirmation = new Confirmation(confirmationCreated);
			_confirmationRepository.SaveConfirmation(confirmation);
		}

		private void When(ConfirmationCreatedEvent confirmationCreatedEvent)
		{
			_confirmationSender.Send(confirmationCreatedEvent.ConfirmationKey);

			var confirmation = _confirmationRepository.GetConfirmation(confirmationCreatedEvent.ConfirmationKey);

			confirmation.TransmitToPending(confirmationCreatedEvent.SagaId);
			_confirmationRepository.SaveConfirmation(confirmation);
		}

		private void When(ConfirmationTransmittedToPendingEvent confirmationTransmittedToPendingEvent)
		{
			// процесс регистрации пользователя можно считать завершенным!
		}

		private void When(object message)
		{
			throw new InvalidOperationException($"Unknown message {message}");
		}
	}
}
