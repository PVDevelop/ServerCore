using System;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	/// <summary>
	/// Доменная модель - подтверждение
	/// </summary>
	public sealed class Confirmation
	{
		/// <summary>
		/// Уникальный идентификатор.
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// Идентификатор Id пользователя в системе. Уникален в БД.
		/// </summary>
		public string UserId { get; }

		/// <summary>
		/// Ключ подтверждения.
		/// </summary>
		public string Key { get; }

		/// <summary>
		/// Время генерации ключа.
		/// </summary>
		public DateTime CreationTime { get; }

		/// <summary>
		/// Состояние подтверждения.
		/// </summary>
		public ConfirmationState State { get; private set; }

		public Confirmation(string userId, string key, DateTime creationTime)
		{
			if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Not set", nameof(userId));
			if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Not set", nameof(key));
			if (creationTime == default(DateTime)) throw new ArgumentException("Not set", nameof(creationTime));

			Id = Guid.NewGuid().ToString();
			UserId = userId;
			Key = key;
			CreationTime = creationTime;
		}

		/// <summary>
		/// Конструктор используется только при восстановлении из хранилища
		/// </summary>
		internal Confirmation(
			string id, 
			string userId, 
			string key,
			ConfirmationState state, 
			DateTime creationTime)
		{
			if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Not set", nameof(id));
			if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Not set", nameof(userId));
			if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Not set", nameof(key));
			if (creationTime == default(DateTime)) throw new ArgumentException("Not set", nameof(creationTime));

			Id = id;
			UserId = userId;
			Key = key;
			CreationTime = creationTime;
			State = state;
		}

		/// <summary>
		/// Подтверждение пользователя. Если уже подтвержден, бросает <see cref="AlreadyConfirmedException"/>. 
		/// </summary>
		public void Confirm()
		{
			State = ConfirmationState.Confirmed;
		}
	}
}
