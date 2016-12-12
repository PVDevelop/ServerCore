using System;
using System.Text.RegularExpressions;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	/// <summary>
	/// Доменная модель - пользователь.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Уникальный идентификатор.
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// Уникальный почтовый адрес.
		/// </summary>
		public string Email { get; }

		/// <summary>
		/// Кодированный пароль пользователя.
		/// </summary>
		public string Password { get; private set; }

		/// <summary>
		/// Время создания.
		/// </summary>
		public DateTime CreationTime { get; }

		/// <summary>
		/// Состояние пользователя.
		/// </summary>
		public UserState State { get; private set; }

		/// <summary>
		/// Конструктор используется только при восстановлении из хранилища
		/// </summary>
		internal User(
			string id,
			string email,
			string password,
			DateTime creationTime,
			UserState state)
		{
			Id = id;
			Email = email;
			Password = password;
			CreationTime = creationTime;
			State = state;
		}

		/// <summary>
		/// Конструктор создания нового пользователя.
		/// </summary>
		public User(
			string email,
			string password,
			DateTime creationTime)
		{
			ValidateEmail(email);
			ValidatePassword(password);

			Id = Guid.NewGuid().ToString();
			Email = email;
			EncryptAndSetPassword(password);
			CreationTime = creationTime;
		}

		private void EncryptAndSetPassword(string plainPassword)
		{
			var salt = BCrypt.Net.BCrypt.GenerateSalt();
			var password = BCrypt.Net.BCrypt.HashPassword(plainPassword, salt);
			Password = password;
		}

		private static void ValidateEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));

			if (!Regex.IsMatch(email, @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,6})+$", RegexOptions.IgnoreCase))
			{
				throw new InvalidEmailFormatException(email);
			}
		}

		private static void ValidatePassword(string password)
		{
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", password);

			if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{7,25}$", RegexOptions.IgnoreCase))
			{
				throw new InvalidPasswordFormatException();
			}
		}

		/// <summary>
		/// Вход пользователя в сеть.
		/// </summary>
		/// <param name="plainPassword">Незакодированный пароль</param>
		public void SignIn(string plainPassword)
		{
			if (State == UserState.WaitingForCreationConfirm)
			{
				throw new UserWaitingForCreationConfirmException(Email);
			}

			if (!BCrypt.Net.BCrypt.Verify(plainPassword, Password))
			{
				throw new InvalidPasswordException(Email);
			}

			State = UserState.SignedIn;
		}

		/// <summary>
		/// Выход пользователя из сети.
		/// </summary>
		public void SignOut()
		{
			if (State == UserState.WaitingForCreationConfirm)
			{
				throw new UserWaitingForCreationConfirmException(Email);
			}

			State = UserState.SignedOut;	
		}

		/// <summary>
		/// Подтверждение создание пользователя.
		/// </summary>
		public void Confirm()
		{
			if (State != UserState.SignedIn)
			{
				State = UserState.SignedOut;
			}
		}
	}
}
