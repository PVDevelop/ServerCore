using System;
using PVDevelop.UCoach.Domain.Messages;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.SagaProgress;
using PVDevelop.UCoach.Saga;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Application
{
	public class UserService
	{
		private readonly IEventObserver<ISagaEvent> _sagaObserver;

		public UserService(IEventObserver<ISagaEvent> sagaObserver)
		{
			if (sagaObserver == null) throw new ArgumentNullException(nameof(sagaObserver));
			_sagaObserver = sagaObserver;
		}

		/// <summary>
		/// Создает пользователя, отправляя подтверждение регистрации ему на почту.
		/// </summary>
		/// <param name="sagaId">Идентификатор транзакции, по которому можно получить результат исполнения.</param>
		/// <param name="email">Почтовый адрес пользователя.</param>
		/// <param name="password">Пароль пользователя.</param>
		public void CreateUser(SagaId sagaId, string email, string password)
		{
			var message = new CreateUserMessage(
				sagaId, 
				new UserCreationProgress(UserCreationStatus.Pending),  
				email, 
				password);
			_sagaObserver.HandleEvent(message);
		}

		public void ConfirmUser(SagaId sagaId, ConfirmationKey confirmaiotKey)
		{
			var message = new ConfirmUserMessage(
				sagaId,
				new UserConfirmationProgress(UserConfirmationStatus.Pending), 
				confirmaiotKey);
			_sagaObserver.HandleEvent(message);
		}
	}
}
