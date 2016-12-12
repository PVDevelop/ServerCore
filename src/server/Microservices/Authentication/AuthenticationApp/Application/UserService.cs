using System;
using Microsoft.Extensions.Configuration;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port.Exceptions;
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
			try
			{
				var user = new User(
					email,
					password,
					_utcTimeProvider.UtcNow);
				_userRepository.Insert(user);

				var confirmationKey = _confirmationKeyGenerator.Generate();

				var confirmation = new Confirmation(
					userId: user.Id,
					key: confirmationKey,
					creationTime: _utcTimeProvider.UtcNow);
				_confirmationRepository.Insert(confirmation);

				var url = string.Format(_confirmationUrl, confirmation.Key);
				_confirmationProducer.Produce(email, url);
			}
			catch (DuplicateEmailException)
			{
				throw new ApplicationException("Указанный почтовый адрес уже кем-то используется.");
			}
		}

		public void ConfirmUserRegistration(string confirmationKey)
		{
			if (string.IsNullOrWhiteSpace(confirmationKey)) throw new ArgumentException("Not set", nameof(confirmationKey));

			var confirmation = _confirmationRepository.FindByConfirmationKey(confirmationKey);
			if (confirmation == null)
			{
				throw new ConfirmationNotFoundException(confirmationKey);
			}

			confirmation.Confirm();
			_confirmationRepository.Update(confirmation);

			var user = _userRepository.GetById(confirmation.UserId);
			if (user == null)
			{
				throw new ApplicationException("Пользователь не зарегистрирован.");
			}

			user.Confirm();
			_userRepository.Update(user);

			var userSession = _userSessionRepository.GetLastSession(confirmation.UserId);
			if (userSession == null)
			{
				userSession =
					new UserSession(confirmation.UserId);
				_userSessionRepository.Insert(userSession);
			}
		}

		public AccessToken SignIn(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			var user = _userRepository.GetByEmail(email);
			if (user == null)
			{
				throw new ApplicationException("Пользователь не зарегистрирован.");
			}

			try
			{
				user.SignIn(password);
			}
			catch (UserWaitingForCreationConfirmException)
			{
				throw new ApplicationException(@"Пользователь не подтвержден. 
Для подтверждения перейдите по ссылке, указанной в отправленном на почту письме.");
			}
			catch (InvalidPasswordException)
			{
				throw new ApplicationException("Пароль указан неверно.");
			}

			_userRepository.Update(user);

			var session = _userSessionRepository.GetLastSession(user.Id);
			if(session == null)
			{
				throw new UserSessionNotStartedException(user.Email);
			}

			session.Activate();
			_userSessionRepository.Update(session);

			return session.GenerateToken(_utcTimeProvider.UtcNow);
		}

		public void ValidateToken(AccessToken accessToken)
		{
			if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));

			var session = _userSessionRepository.GetLastSession(accessToken.UserId);
			if(session == null)
			{
				throw new UserSessionNotStartedException(accessToken.UserId);
			}

			session.Validate(accessToken, _utcTimeProvider);
		}

		public void SignOut(AccessToken accessToken)
		{
			if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));

			var user = _userRepository.GetById(accessToken.UserId);
			if (user == null)
			{
				throw new ApplicationException("Пользователь не зарегистрирован");
			}

			user.SignOut();
			_userRepository.Update(user);

			var session = _userSessionRepository.GetLastSession(accessToken.UserId);
			if (session == null)
			{
				throw new UserSessionNotStartedException(accessToken.UserId);
			}

			session.Inactivate();
			_userSessionRepository.Update(session);
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
