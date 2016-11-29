using System;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions;
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
		/// Состояние активности сессии.
		/// </summary>
		public SessionState State { get; private set; }

		public UserSession(
			string userId)
		{
			if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Not set", nameof(userId));

			Id = Guid.NewGuid().ToString();
			UserId = userId;
		}

		/// <summary>
		/// Контсруктор, используемый для воостановления пользователя из хранилища.
		/// </summary>
		internal UserSession(string id, string userId, SessionState state)
		{
			Id = id;
			UserId = userId;
			State = state;
		}

		/// <summary>
		/// Генерирует новый токен доступа.
		/// </summary>
		/// <returns>Токен доступа.</returns>
		public AccessToken GenerateToken(DateTime utcNow)
		{
			if (State != SessionState.Active)
			{
				throw new InactiveSessionException();
			}

			var salt = BCrypt.Net.BCrypt.GenerateSalt();
			var token = BCrypt.Net.BCrypt.HashPassword(Id, salt);

			return new AccessToken(UserId, token, utcNow + TokenExpirationPeriod);
		}

		/// <summary>
		/// Переводит сессию в состояние активности.
		/// </summary>
		public void Activate()
		{
			State = SessionState.Active;
		}

		/// <summary>
		/// Переводит сессию в состояние неактивности.
		/// </summary>
		public void Inactivate()
		{
			State = SessionState.Inactive;
		}

		/// <summary>
		/// Проверяет токен и возвращает признак его валидности.
		/// </summary>
		/// <param name="accessToken">Валидируемый токен.</param>
		/// <param name="utcTimeProvider">Провайдер UTC времени.</param>
		/// <returns>Признак валидности токена.</returns>
		public void Validate(AccessToken accessToken, IUtcTimeProvider utcTimeProvider)
		{
			if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));
			if (utcTimeProvider == null) throw new ArgumentNullException(nameof(utcTimeProvider));

			if(State != SessionState.Active)
			{
				throw new InactiveSessionException();
			}

			var utcNow = utcTimeProvider.UtcNow;

			if (utcNow > accessToken.Expiration)
			{
				throw new InvalidTokenException("Token expired.");
			}

			if(!BCrypt.Net.BCrypt.Verify(Id, accessToken.Token))
			{
				throw new InvalidTokenException("Incorrect token hash.");
			}
		}
	}
}
