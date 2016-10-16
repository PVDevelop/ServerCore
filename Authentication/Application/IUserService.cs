namespace PVDevelop.UCoach.Authentication.Application
{
	public interface IUserService
	{
		/// <summary>
		/// Создать нового пользователя и выслать подтверждение регистрации
		/// </summary>
		/// <param name="email">Почтовый адрес пользователя</param>
		/// <param name="password">Пароль пользователя</param>
		/// <param name="url4Confirmation">Ссылка на подтверждение создания пользователя</param>
		void CreateUser(string email, string password, string url4Confirmation);
	}
}
