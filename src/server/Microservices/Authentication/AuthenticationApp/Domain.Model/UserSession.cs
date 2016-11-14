using System;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	/// <summary>
	/// Представляет сессию пользователя. Сессия порождаем токены пользователя, а также позволяет валидировать токены.
	/// </summary>
	public class UserSession
	{
		internal static readonly TimeSpan TokenExpirationPeriod = TimeSpan.FromDays(1);

		/// <summary>
		/// Уникальный, секретный ключ сессии (в контексте пользователя), который используется для генерации и проверки токена.
		/// Для генерации токена используется алгоритм Bcrypt с этим идентификатором в качестве входной последовательности.
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// Время истечения сессии. После окончания действия все токены становятся недействительны.
		/// </summary>
		public DateTime Expiration { get; }

		public UserSession(
			IUserSessionGenerator userSessionGenerator,
			IUtcTimeProvider utcTimeProvider)
		{
			if (userSessionGenerator == null) throw new ArgumentNullException(nameof(userSessionGenerator));
			if (utcTimeProvider == null) throw new ArgumentNullException(nameof(utcTimeProvider));

			Id = userSessionGenerator.Generate();
			Expiration = utcTimeProvider.UtcNow + TokenExpirationPeriod;
		}

		/// <summary>
		/// Конструктор используется только для восстановления из хранилища
		/// </summary>
		/// <param name="id"></param>
		/// <param name="expiration"></param>
		internal UserSession(string id, DateTime expiration)
		{
			Id = id;
			Expiration = expiration;
		}

		/// <summary>
		/// Генерирует новый токен доступа.
		/// </summary>
		/// <returns>Токен доступа.</returns>
		public AccessToken GenerateToken()
		{
			var salt = BCrypt.Net.BCrypt.GenerateSalt();
			var token = BCrypt.Net.BCrypt.HashPassword(Id, salt);

			return new AccessToken(token, Expiration);
		}

		/// <summary>
		/// Проверяет токен и возвращает признак его валидности.
		/// </summary>
		/// <param name="accessToken">Валидируемый токен.</param>
		/// <returns>Признак валидности токена.</returns>
		public bool Validate(AccessToken accessToken, IUtcTimeProvider utcTimeProvider)
		{
			if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));
			if (utcTimeProvider == null) throw new ArgumentNullException(nameof(utcTimeProvider));

			var utcNow = utcTimeProvider.UtcNow;

			if (utcNow > Expiration)
			{
				return false;
			}

			if (utcNow > accessToken.Expiration)
			{
				return false;
			}

			if (!BCrypt.Net.BCrypt.Verify(Id, accessToken.Token))
			{
				return false;
			}

			return true;
		}
	}
}
