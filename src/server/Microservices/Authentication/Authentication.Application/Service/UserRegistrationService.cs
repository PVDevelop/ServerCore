using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;

namespace PVDevelop.UCoach.Application.Service
{
	public class UserRegistrationService
	{
		private readonly IUserRepository _userRepository;

		public UserRegistrationService(IUserRepository userRepository)
		{
			if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));

			_userRepository = userRepository;
		}

		/// <summary>
		/// Регистрирует пользователя, отправляя подтверждение регистрации ему на почту.
		/// </summary>
		/// <param name="userId">Идентификатор транзакции, по которому можно получить результат исполнения.</param>
		/// <param name="email">Почтовый адрес пользователя.</param>
		/// <param name="password">Пароль пользователя.</param>
		public void RegisterUser(UserId userId, string email, string password)
		{
			var user = new User(userId, email, password);
			_userRepository.SaveUser(user);
		}
	}
}
