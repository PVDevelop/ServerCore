using System;
using PVDevelop.UCoach.AuthenticationApp.Application;
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
		/// Идентификатор пользователя, которому принадлежит сессия.
		/// </summary>
		public string UserId { get; }

		/// <summary>
		/// Время истечения сессии. После окончания действия все токены становятся недействительны.
		/// </summary>
		public DateTime Expiration { get; }

		public UserSession(
			string userId,
			DateTime utcNow)
		{
			if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Not set", nameof(userId));
			if (utcNow == default(DateTime)) throw new ArgumentException("Not set", nameof(utcNow));
			if (utcNow.Kind != DateTimeKind.Utc) throw new ArgumentException("Not UTC", nameof(utcNow));

			Id = Guid.NewGuid().ToString();
			UserId = userId;
			Expiration = utcNow + TokenExpirationPeriod;
		}

		/// <summary>
		/// Контсруктор, используемый для воостановления пользователя из хранилища.
		/// </summary>
		internal UserSession(string id, string userId, DateTime expiration)
		{
			Id = id;
			UserId = userId;
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

			return new AccessToken(UserId, token, Expiration);
		}

		/// <summary>
		/// Проверяет токен и возвращает признак его валидности.
		/// </summary>
		/// <param name="accessToken">Валидируемый токен.</param>
		/// <param name="utcTimeProvider">Провайдер UTC времени.</param>
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
