using System;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.Mongo.User
{
	[MongoCollection("Users")]
	public class MongoUser
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Почтовый адерс пользователя. Уникален в БД.
		/// </summary>
		[MongoIndexName("email")]
		public string Email { get; set; }

		/// <summary>
		/// Пароль пользователя. Закодирован.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Время создания пользователя.
		/// </summary>
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// Состояние пользователя.
		/// </summary>
		public UserState State { get; set; }
	}
}
