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

		public string Id { get; set; }

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
		/// Время создания пользователя
		/// </summary>
		public DateTime CreationTime { get; set; }

		public ConfirmationStatus Status { get; set; }

		public MongoUser()
		{
			Version = VERSION;
		}
	}
}
