using System;
using PVDevelop.UCoach.Domain.Messages;
using PVDevelop.UCoach.Infrastructure;
using PVDevelop.UCoach.Infrastructure.Port;

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
		/// <param name="sagaId">Идентификатор саги, по которому можно получить результат исполнения.</param>
		/// <param name="email">Почтовый адрес пользователя.</param>
		/// <param name="password">Пароль пользователя.</param>
		public void CreateUser(Guid sagaId, string email, string password)
		{
			var message = new CreateUserMessage(sagaId, email, password);
			_messagePublisher.Publish(message);
		}
	}
}
