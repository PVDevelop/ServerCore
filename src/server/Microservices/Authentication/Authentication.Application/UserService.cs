using System;
using PVDevelop.UCoach.Infrastructure;
using PVDevelop.UCoach.Domain.Service;

namespace PVDevelop.UCoach.Application
{
	public class UserService
	{
		/// <summary>
		/// Создает пользователя, отправляя подтверждение регистрации ему на почту.
		/// </summary>
		/// <param name="transactionId">Идентификатор транзакции, по которому можно получить результат исполнения.</param>
		/// <param name="email">Почтовый адрес пользователя.</param>
		/// <param name="password">Пароль пользователя.</param>
		public void CreateUser(Guid transactionId, string email, string password)
		{
			throw new NotImplementedException();
		}
	}
}
