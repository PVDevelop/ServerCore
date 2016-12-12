using System;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.UserAggregate;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port
{
	/// <summary>
	/// Репозиторий пользователей.
	/// </summary>
	public interface IUserRepository2
	{
		/// <summary>
		/// Добавляет нового или создает имеющегося пользователя.
		/// </summary>
		/// <param name="user">Добавляемый или обновляемый пользователь.</param>
		void AddUpdate(User user);

		/// <summary>
		/// Возвращает пользователя по идентификатору.
		/// </summary>
		User GetById(Guid id);

		/// <summary>
		/// Возвращает пользователя по почтовому адресу.
		/// </summary>
		User GetByEmail(string email);
	}
}
