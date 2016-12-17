﻿using System;
using PVDevelop.UCoach.Domain.Messages;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Service
{
	public class UserService : ISagaMessageConsumer
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfirmationRepository _confirmationRepository;
		private readonly IConfirmationKeyGenerator _confirmationKeyGenerator;
		private readonly IConfirmationSender _confirmationSender;
		private readonly IUserProcessRepository _userProcessRepository;

		public UserService(
			IUserRepository userRepository,
			IConfirmationRepository confirmationRepository,
			IConfirmationKeyGenerator confirmationKeyGenerator,
			IConfirmationSender confirmationSender,
			IUserProcessRepository userProcessRepository)
		{
			if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
			if (confirmationRepository == null) throw new ArgumentNullException(nameof(confirmationRepository));
			if (confirmationKeyGenerator == null) throw new ArgumentNullException(nameof(confirmationKeyGenerator));
			if (confirmationSender == null) throw new ArgumentNullException(nameof(confirmationSender));
			if (userProcessRepository == null) throw new ArgumentNullException(nameof(userProcessRepository));
			_userRepository = userRepository;
			_confirmationRepository = confirmationRepository;
			_confirmationKeyGenerator = confirmationKeyGenerator;
			_confirmationSender = confirmationSender;
			_userProcessRepository = userProcessRepository;
		}

		public void Consume(ISagaMessage message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));
			When((dynamic) message);
		}

		private void When(CreateUserMessage createUserMessage)
		{
			var userCreated = new UserCreatedEvent(
				createUserMessage.SagaId,
				new UserId(createUserMessage.SagaId),
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

			var @event = new ConfirmationTransmittedToPendingEvent(
				confirmationCreatedEvent.SagaId,
				confirmation.Id);

			confirmation.Mutate(@event);
			_confirmationRepository.SaveConfirmation(confirmation);
		}

		private void When(ConfirmationTransmittedToPendingEvent confirmationTransmittedToPendingEvent)
		{
			// процесс регистрации пользовател можно считать завершенным!
			var userCreationResult = new UserCreationResult(
				confirmationTransmittedToPendingEvent.SagaId,
				UserCreationState.Succeeded);

			_userProcessRepository.SetUserCreationResult(userCreationResult);
		}

		private void When(object message)
		{
			throw new InvalidOperationException($"Unknown message {message}");
		}
	}
}
