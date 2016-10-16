using System;
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

			//_logger.Debug("Создаю пользователя '{0}'.", email);

			var user = new User(
				_keyGeneratorService.GenerateUserId(),
				email,
				password,
				_utcTimeProvider.UtcNow);

			_userRepository.Insert(user);

			//_logger.Debug("Создаю ключ подтверждения для пользователя '{0}'.", email);
			var confirmation = new Confirmation(
				userId: user.Id,
				key: _keyGeneratorService.GenerateUserId(),
				creationTime: _utcTimeProvider.UtcNow);
			_confirmationRepository.Replace(confirmation);

			//_logger.Debug("Отправление ключа пользователю");

			var url = Format(url4Confirmation, confirmation.Key);

			_confirmationProducer.Produce(email, url);

			//_logger.Info("Пользователь {0} создан.", email);
		}
	}
}
