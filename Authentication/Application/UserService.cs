using System;
using Microsoft.Extensions.Logging;
using NLog;
using PVDevelop.UCoach.Authentication.Domain.Model;
using PVDevelop.UCoach.Authentication.Infrastructure;
using PVDevelop.UCoach.Timing;
using static System.String;

namespace PVDevelop.UCoach.Authentication.Application
{
	public class UserService : IUserService
	{
		private readonly IKeyGeneratorService _keyGeneratorService;
		private readonly IUtcTimeProvider _utcTimeProvider;
		private readonly IUserRepository _userRepository;
		private readonly IConfirmationRepository _confirmationRepository;
		private readonly IConfirmationProducer _confirmationProducer;
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public UserService(
			IKeyGeneratorService keyGeneratorService,
			IUtcTimeProvider utcTimeProvider,
			IUserRepository userRepository,
			IConfirmationRepository confirmationRepository,
			IConfirmationProducer confirmationProducer)
		{
			if (keyGeneratorService == null) throw new ArgumentNullException(nameof(keyGeneratorService));
			if (utcTimeProvider == null) throw new ArgumentNullException(nameof(utcTimeProvider));
			if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
			if (confirmationRepository == null) throw new ArgumentNullException(nameof(confirmationRepository));
			if (confirmationProducer == null) throw new ArgumentNullException(nameof(confirmationProducer));

			_keyGeneratorService = keyGeneratorService;
			_utcTimeProvider = utcTimeProvider;
			_userRepository = userRepository;
			_confirmationRepository = confirmationRepository;
			_confirmationProducer = confirmationProducer;
		}

		public void CreateUser(string email, string password, string url4Confirmation)
		{
			if (IsNullOrWhiteSpace(url4Confirmation))
			{
				throw new ArgumentException("Not set", nameof(url4Confirmation));
			}

			_logger.Debug($"Создаю пользователя '{email}'.");

			var user = new User(
				_keyGeneratorService.GenerateUserId(),
				email,
				password,
				_utcTimeProvider.UtcNow);
			_userRepository.Insert(user);

			_logger.Debug($"Создаю ключ подтверждения для пользователя '{email}'.");
			var confirmation = new Confirmation(
				userId: user.Id,
				key: _keyGeneratorService.GenerateUserId(),
				creationTime: _utcTimeProvider.UtcNow);
			_confirmationRepository.Insert(confirmation);

			_logger.Debug("Отправление ключа пользователю");

			var url = Format(url4Confirmation, confirmation.Key);
			_confirmationProducer.Produce(email, url);

			_logger.Info($"Пользователь '{email}' создан.");
		}
	}
}
