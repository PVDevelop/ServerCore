using PVDevelop.UCoach.AuthenticationApp.Domain.Model;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port
{
	public interface IUserSessionRepository
	{
		/// <summary>
		/// Возвращает последнюю сессию пользователя. Если не находит, возвращает null.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя.</param>
		UserSession GetLastSession(string userId);

		/// <summary>
		/// Добавляет новую сессию пользователя.
		/// </summary>
		/// <param name="userSession">Сессия пользователя.</param>
		void Insert(UserSession userSession);

		/// <summary>
		/// Обновить имеющуюся сессию пользователя.
		/// </summary>
		/// <param name="userSession">Сессия пользователя.</param>
		void Update(UserSession userSession);
	}
}
