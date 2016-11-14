using System;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Mongo
{
	[MongoCollection("Users")]
	[MongoDataVersion(VERSION)]
	public class MongoUser
	{
		/// <summary>
		/// Текущая версия документа
		/// </summary>
		public const int VERSION = 1;

		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Версия формата документа.
		/// </summary>
		public int Version { get; private set; }

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
		/// Состояние пользователя.
		/// </summary>
		public UserState State { get; set; }

		/// <summary>
		/// Время создания пользователя.
		/// </summary>
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// Токен авторизации.
		/// </summary>
		public MongoUserSession Session { get; set; }

		public MongoUser()
		{
			Version = VERSION;
		}
	}
}
