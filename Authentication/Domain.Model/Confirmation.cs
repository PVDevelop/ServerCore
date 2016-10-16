using System;

namespace PVDevelop.UCoach.Authentication.Domain.Model
{
	/// <summary>
	/// Доменная модель - подтверждение
	/// </summary>
	public sealed class Confirmation
	{
		/// <summary>
		/// Идентификатор Id пользователя в системе. Уникален в БД.
		/// </summary>
		public string UserId { get; }

		/// <summary>
		/// Ключ подтверждения
		/// </summary>
		public string Key { get; }

		/// <summary>
		/// Время генерации ключа
		/// </summary>
		public DateTime CreationTime { get; }

		public Confirmation(string userId, string key, DateTime creationTime)
		{
			if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Not set", nameof(userId));
			if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Not set", nameof(key));
			if (creationTime == default(DateTime)) throw new ArgumentException("Not set", nameof(creationTime));

			UserId = userId;
			Key = key;
			CreationTime = creationTime;
		}
	}
}
