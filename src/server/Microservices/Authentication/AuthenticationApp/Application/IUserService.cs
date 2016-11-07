namespace PVDevelop.UCoach.AuthenticationApp.Application
{
	public interface IUserService
	{
		/// <summary>
		/// Создать нового пользователя и выслать подтверждение регистрации
		/// </summary>
		/// <param name="email">Почтовый адрес пользователя</param>
		/// <param name="password">Пароль пользователя</param>
		void CreateUser(string email, string password);

		/// <summary>
		/// Подтвердить регистрацию пользователя
		/// </summary>
		/// <param name="confirmationKey">Клю подтверждения</param>
		void ConfirmUserRegistration(string confirmationKey);
	}
}
