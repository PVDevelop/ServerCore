using System;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.Mongo.Confirmation
{
	[MongoCollection("Confirmations")]
	public class MongoConfirmation
	{
		/// <summary>
		/// Идентификатор подтверждения.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Версия формата документа.
		/// </summary>
		public int Version { get; set; }

		/// <summary>
		/// Ключ подтверждения создания пользователя.
		/// </summary>
		[MongoIndexName("key")]
		public string Key { get; set; }

		/// <summary>
		/// Идентификатор пользователя в системе.
		/// </summary>
		[MongoIndexName("userId")]
		public string UserId { get; set; }

		/// <summary>
		/// Состояние подтверждения.
		/// </summary>
		public ConfirmationState State { get; set; }

		/// <summary>
		/// Время генерации ключа.
		/// </summary>
		public DateTime CreationTime { get; set; }
	}
}
