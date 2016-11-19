using PVDevelop.UCoach.AuthenticationApp.Domain.Model;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port
{
	public interface IUserSessionRepository
	{
		/// <summary>
		/// Возвращает сессию пользователя. Если не находит, возвращает null.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя.</param>
		UserSession[] GetByUserId(string userId);

		/// <summary>
		/// Добавляет новую сессию пользователя.
		/// </summary>
		/// <param name="userSession">Сессия пользователя.</param>
		void Insert(UserSession userSession);
	}
}
