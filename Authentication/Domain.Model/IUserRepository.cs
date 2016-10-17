namespace PVDevelop.UCoach.Authentication.Domain.Model
{
	public interface IUserRepository
	{
		/// <summary>
		/// Добавление нового пользователя в БД
		/// </summary>
		void Insert(User user);
	}
}
