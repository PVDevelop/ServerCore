using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port;
using PVDevelop.UCoach.Logging;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.AuthenticationApp.Application
{
	public class UserService : IUserService
	{
		private readonly IConfirmationKeyGenerator _confirmationKeyGenerator;
		private readonly IUtcTimeProvider _utcTimeProvider;
		private readonly IUserRepository _userRepository;
		private readonly IUserSessionRepository _userSessionRepository;
		private readonly IConfirmationRepository _confirmationRepository;
		private readonly IConfirmationProducer _confirmationProducer;
		private readonly ILogger _logger = LoggerHelper.GetLogger<UserService>();
		private readonly string _confirmationUrl;

		public UserService(
			IConfirmationKeyGenerator confirmationKeyGenerator,
			IUtcTimeProvider utcTimeProvider,
			IUserRepository userRepository,
			IUserSessionRepository userSessionRepository,
			IConfirmationRepository confirmationRepository,
			IConfirmationProducer confirmationProducer,
			IConfigurationRoot configuration)
		{
			if (confirmationKeyGenerator == null) throw new ArgumentNullException(nameof(confirmationKeyGenerator));
			if (utcTimeProvider == null) throw new ArgumentNullException(nameof(utcTimeProvider));
			if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
			if (userSessionRepository == null) throw new ArgumentNullException(nameof(userSessionRepository));
			if (confirmationRepository == null) throw new ArgumentNullException(nameof(confirmationRepository));
			if (confirmationProducer == null) throw new ArgumentNullException(nameof(confirmationProducer));
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			_confirmationKeyGenerator = confirmationKeyGenerator;
			_utcTimeProvider = utcTimeProvider;
			_userRepository = userRepository;
			_userSessionRepository = userSessionRepository;
			_confirmationRepository = confirmationRepository;
			_confirmationProducer = confirmationProducer;

			_confirmationUrl = GetConfirmationUrl(configuration);
		}

		public void CreateUser(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			_logger.Debug($"Создаю пользователя '{email}'.");

			var user = new User(
				email,
				password,
				_utcTimeProvider.UtcNow);
			_userRepository.Insert(user);

			_logger.Debug($"Создаю ключ подтверждения для пользователя '{email}'.");

			var confirmationKey = _confirmationKeyGenerator.Generate();

			var confirmation = new Confirmation(
				userId: user.Id,
				key: confirmationKey,
				creationTime: _utcTimeProvider.UtcNow);
			_confirmationRepository.Insert(confirmation);

			_logger.Debug("Отправление ключа пользователю");

			var url = string.Format(_confirmationUrl, confirmation.Key);
			_confirmationProducer.Produce(email, url);

			_logger.Info($"Пользователь '{email}' создан.");
		}

		public AccessToken ConfirmUserRegistration(string confirmationKey)
		{
			if (string.IsNullOrWhiteSpace(confirmationKey)) throw new ArgumentException("Not set", nameof(confirmationKey));

			_logger.Debug($"Подтверждение регистрации пользователя с ключом '{confirmationKey}'.");

			var confirmation = _confirmationRepository.FindByConfirmationKey(confirmationKey);
			if (confirmation == null)
			{
				throw new ConfirmationNotFoundException(confirmationKey);
			}

			confirmation.Confirm();

			_logger.Debug($"Сохраняю подтверждение '{confirmation.Key}'.");
			_confirmationRepository.Update(confirmation);

			var userSession = _userSessionRepository.GetByUserId(confirmation.UserId).LastOrDefault();
			if (userSession == null)
			{
				_logger.Debug($"Создаю новую сессию пользователя '{confirmation.UserId}'");
				userSession =
					new UserSession(confirmation.UserId, _utcTimeProvider.UtcNow);

				_logger.Debug($"Сохраняю сессию пользователя '{userSession.UserId}'");
				_userSessionRepository.Insert(userSession);
			}
			else
			{
				_logger.Debug($"Сессия пользователя '{userSession.UserId}' уже существует, Id сессии - '{userSession.Id}'");
			}

			return userSession.GenerateToken();
		}

		private static string GetConfirmationUrl(IConfigurationRoot configuration)
		{
			var confirmationUrl = configuration.GetConnectionString("ConfirmationUrl");

			if (string.IsNullOrWhiteSpace(confirmationUrl))
			{
				throw new InvalidOperationException("Confirmation url not set");
			}

			return confirmationUrl;
		}
	}
}
