using System;
using PVDevelop.UCoach.Domain.Messages;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Infrastructure;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Application
{
	public class UserService
	{
		private readonly ISagaEventStoreMessagePublisher _messagePublisher;

		public UserService(ISagaEventStoreMessagePublisher messagePublisher)
		{
			if (messagePublisher == null) throw new ArgumentNullException(nameof(messagePublisher));
			_messagePublisher = messagePublisher;
		}

		/// <summary>
		/// Создает пользователя, отправляя подтверждение регистрации ему на почту.
		/// </summary>
		/// <param name="transactionId">Идентификатор транзакции, по которому можно получить результат исполнения.</param>
		/// <param name="email">Почтовый адрес пользователя.</param>
		/// <param name="password">Пароль пользователя.</param>
		public void CreateUser(Guid transactionId, string email, string password)
		{
			var message = new CreateUserMessage(new SagaId(transactionId), email, password);
			_messagePublisher.Publish(message);
		}

		public void ConfirmUser(Guid transactionId, string confirmaiotKey)
		{
			var message = new ConfirmUserMessage(
				new SagaId(transactionId),
				new ConfirmationKey(confirmaiotKey));
			_messagePublisher.Publish(message);
		}
	}
}
