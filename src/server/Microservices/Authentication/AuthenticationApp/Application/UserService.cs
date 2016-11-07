﻿using System;
using Microsoft.Extensions.Configuration;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure;
using PVDevelop.UCoach.Logging;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.AuthenticationApp.Application
{
	public class UserService : IUserService
	{
		private readonly IKeyGeneratorService _keyGeneratorService;
		private readonly IUtcTimeProvider _utcTimeProvider;
		private readonly IUserRepository _userRepository;
		private readonly IConfirmationRepository _confirmationRepository;
		private readonly IConfirmationProducer _confirmationProducer;
		private readonly ILogger _logger = LoggerHelper.GetLogger<UserService>();
		private readonly string _confirmationUrl;

		public UserService(
			IKeyGeneratorService keyGeneratorService,
			IUtcTimeProvider utcTimeProvider,
			IUserRepository userRepository,
			IConfirmationRepository confirmationRepository,
			IConfirmationProducer confirmationProducer,
			IConfigurationRoot configuration)
		{
			if (keyGeneratorService == null) throw new ArgumentNullException(nameof(keyGeneratorService));
			if (utcTimeProvider == null) throw new ArgumentNullException(nameof(utcTimeProvider));
			if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
			if (confirmationRepository == null) throw new ArgumentNullException(nameof(confirmationRepository));
			if (confirmationProducer == null) throw new ArgumentNullException(nameof(confirmationProducer));
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			_keyGeneratorService = keyGeneratorService;
			_utcTimeProvider = utcTimeProvider;
			_userRepository = userRepository;
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
			var confirmation = new Confirmation(
				userId: user.Id,
				key: _keyGeneratorService.GenerateConfirmationKey(),
				creationTime: _utcTimeProvider.UtcNow);
			_confirmationRepository.Insert(confirmation);

			_logger.Debug("Отправление ключа пользователю");

			var url = string.Format(_confirmationUrl, confirmation.Key);
			_confirmationProducer.Produce(email, url);

			_logger.Info($"Пользователь '{email}' создан.");
		}

		public void ConfirmUserRegistration(string confirmationKey)
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

			var user = _userRepository.GetById(confirmation.UserId);
			if (user == null)
			{
				// todo: здесь надо откатить подтверждение.
				throw new UserNotFoundException(confirmation.UserId);
			}

			user.Confirm();

			_logger.Debug($"Сохраняю пользователя '{user.Email}'");
			_userRepository.Update(user);
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
