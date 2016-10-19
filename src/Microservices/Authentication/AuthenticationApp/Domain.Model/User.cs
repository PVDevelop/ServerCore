﻿using System;
using System.Text.RegularExpressions;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	/// <summary>
	/// Доменная модель - пользователь
	/// </summary>
	public class User
	{
		/// <summary>
		/// Уникальный идентификатор
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// Уникальный почтовый адрес
		/// </summary>
		public string Email { get; }

		/// <summary>
		/// Кодированный пароль пользователя
		/// </summary>
		public string Password { get; private set; }

		/// <summary>
		/// Время создания
		/// </summary>
		public DateTime CreationTime { get; }

		/// <summary>
		/// Статус подтверждения пользователя
		/// </summary>
		public ConfirmationStatus ConfirmationStatus { get; private set; }

		/// <summary>
		/// Конструктор используется только при восстановлении из хранилища
		/// </summary>
		internal User(
			string id,
			string email,
			string password,
			DateTime creationTime,
			ConfirmationStatus confirmationStatus)
		{
			Id = id;
			Email = email;
			Password = password;
			CreationTime = creationTime;
			ConfirmationStatus = confirmationStatus;
		}

		public User(
			string id,
			string email,
			string password,
			DateTime creationTime)
		{
			if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Not set", nameof(id));

			ValidateEmail(email);
			ValidatePassword(email, password);

			Id = id;
			Email = email;
			EncryptAndSetPassword(password);
			CreationTime = creationTime;
			ConfirmationStatus = ConfirmationStatus.Unconfirmed;
		}

		private void EncryptAndSetPassword(string plainPassword)
		{
			var salt = BCrypt.Net.BCrypt.GenerateSalt();
			var password = BCrypt.Net.BCrypt.HashPassword(plainPassword, salt);
			Password = password;
		}

		private void ValidateEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));

			if (!Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
			{
				throw new InvalidEmailFormatException(email);
			}
		}

		public void ValidatePassword(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", password);

			if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{7,15}$", RegexOptions.IgnoreCase))
			{
				throw new InvalidPasswordFormatException(email);
			}
		}

		/// <summary>
		/// Проверка пароля.
		/// </summary>
		/// <param name="plainPassword">Незакодированный пароль</param>
		public void CheckPassword(string plainPassword)
		{
			if (!BCrypt.Net.BCrypt.Verify(plainPassword, Password))
			{
				throw new InvalidPasswordException(Email);
			}
		}

		/// <summary>
		/// Установка статуса подтверждение
		/// </summary>
		public void Confirm()
		{
			ConfirmationStatus = ConfirmationStatus.Confirmed;
		}
	}
}