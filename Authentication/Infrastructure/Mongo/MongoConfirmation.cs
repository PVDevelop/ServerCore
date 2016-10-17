using System;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Mongo
{
	[MongoCollection("Confirmations")]
	[MongoDataVersion(VERSION)]
	public class MongoConfirmation
	{
		/// <summary>
		/// Текущая версия документа
		/// </summary>
		public const int VERSION = 1;

		public int Version { get; set; }

		/// <summary>
		/// Идентификатор пользователя в системе
		/// </summary>
		[MongoIndexName("userId")]
		public string UserId { get; set; }

		/// <summary>
		/// Ключ подтверждения создания пользователя
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// Время генерации ключа
		/// </summary>
		public DateTime CreationTime { get; set; }

		public MongoConfirmation()
		{
			Version = VERSION;
		}
	}
}
