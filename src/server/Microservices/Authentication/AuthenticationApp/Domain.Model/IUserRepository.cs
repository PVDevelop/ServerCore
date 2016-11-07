﻿namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	public interface IUserRepository
	{
		/// <summary>
		/// Находит пользователя по его Id. Если не найден, возвращает null.
		/// </summary>
		User GetById(string userId);

		/// <summary>
		/// Добавление нового пользователя.
		/// </summary>
		void Insert(User user);

		/// <summary>
		/// Обновление имеющегося пользователя.
		/// </summary>
		void Update(User user);
	}
}
