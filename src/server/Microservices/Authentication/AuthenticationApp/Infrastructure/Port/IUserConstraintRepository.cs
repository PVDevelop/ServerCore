using System;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.UserAggregate;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port
{
	/// <summary>
	/// Репозиторий ограничений пользователя. 
	/// Необходим для контроля уникальности определенных полей пользователя,
	/// т.к. используемый механизм EventSourcing не предоставляет быстрый способ проверки ограничений.
	/// </summary>
	public interface IUserConstraintRepository
	{
		/// <summary>
		/// Добавляет ограничения пользователя
		/// </summary>
		/// <param name="user">Пользователь, чьи ограничения удут добавлены</param>
		void Add(User user);

		/// <summary>
		/// Возвращает идентификатор пользователя по почтовому адресу.
		/// </summary>
		Guid GetUserId(string email);
	}
}
