namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	public interface IUserRepository
	{
		/// <summary>
		/// Добавление нового пользователя в БД
		/// </summary>
		void Insert(User user);
	}
}
