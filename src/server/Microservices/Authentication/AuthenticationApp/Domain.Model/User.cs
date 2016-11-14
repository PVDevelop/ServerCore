using System;
using System.Text.RegularExpressions;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	/// <summary>
	/// Доменная модель - пользователь.
	/// </summary>
	public class User
	{
		private static readonly TimeSpan TokenExpiration = TimeSpan.FromDays(1);

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
		/// Состояние пользователя.
		/// </summary>
		public UserState State { get; private set; }

		/// <summary>
		/// Время создания.
		/// </summary>
		public DateTime CreationTime { get; }

		/// <summary>
		/// Токен подтверждения пользователя.
		/// </summary>
		public UserToken Token { get; private set; }

		/// <summary>
		/// Конструктор используется только при восстановлении из хранилища
		/// </summary>
		internal User(
			string id,
			string email,
			string password,
			UserState state,
			DateTime creationTime,
			UserToken token)
		{
			Id = id;
			Email = email;
			Password = password;
			State = state;
			CreationTime = creationTime;
			Token = token;
		}

		public User(
			string email,
			string password,
			DateTime creationTime)
		{
			ValidateEmail(email);
			ValidatePassword(email, password);

			Id = Guid.NewGuid().ToString();
			Email = email;
			EncryptAndSetPassword(password);
			CreationTime = creationTime;
			State = UserState.New;
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

			if (!Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
			{
				throw new InvalidEmailFormatException(email);
			}
		}

		private static void ValidatePassword(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", password);

			if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{7,15}$", RegexOptions.IgnoreCase))
			{
				throw new InvalidPasswordFormatException(email);
			}
		}

		/// <summary>
		/// Проверка некодированного пароля.
		/// </summary>
		/// <param name="plainPassword">Незакодированный пароль</param>
		public void CheckPlainPassword(string plainPassword)
		{
			if (!BCrypt.Net.BCrypt.Verify(plainPassword, Password))
			{
				throw new InvalidPasswordException(Email);
			}
		}

		/// <summary>
		/// Перевод пользователя в состояние "Подтвержден".
		/// </summary>
		/// <returns>Токен авторизации</returns>
		public UserToken Confirm(ITokenGenerator tokenGenerator, IUtcTimeProvider utcTimeProvider)
		{
			if (tokenGenerator == null) throw new ArgumentNullException(nameof(tokenGenerator));
			if (utcTimeProvider == null) throw new ArgumentNullException(nameof(utcTimeProvider));

			var token = tokenGenerator.Generate();
			var expiration = utcTimeProvider.UtcNow + TokenExpiration;
			var userToken = new UserToken(token, expiration);

			Token = userToken;
			State = UserState.Confirmed;

			return Token;
		}
	}
}
