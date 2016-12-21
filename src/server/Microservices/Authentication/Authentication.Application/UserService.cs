using System;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Application
{
	public class UserService
	{
		private readonly IEventObserver<CreateUserRequested> _createUserObserver;

		public UserService(IEventObserver<CreateUserRequested> createUserObserver)
		{
			if (createUserObserver == null) throw new ArgumentNullException(nameof(createUserObserver));

			_createUserObserver = createUserObserver;
		}

		/// <summary>
		/// Создает пользователя, отправляя подтверждение регистрации ему на почту.
		/// </summary>
		/// <param name="userId">Идентификатор транзакции, по которому можно получить результат исполнения.</param>
		/// <param name="email">Почтовый адрес пользователя.</param>
		/// <param name="password">Пароль пользователя.</param>
		public void CreateUser(UserId userId, string email, string password)
		{
			var @event = new CreateUserRequested(userId, email, password);
			_createUserObserver.HandleEvent(@event);
		}
	}
}
