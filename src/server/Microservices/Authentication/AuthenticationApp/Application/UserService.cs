using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions;
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

		public AccessToken ConfirmUserRegistration(string confirmationKey)
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
			if(user == null)
			{
				throw new UserNotFoundException(confirmation.Id);
			}

			user.Confirm();
			_userRepository.Update(user);

			var userSession = _userSessionRepository.GetLastSession(confirmation.UserId);
			if (userSession == null)
			{
				userSession =
					new UserSession(confirmation.UserId);
				userSession.Activate();

				_userSessionRepository.Insert(userSession);
			}
			else
			{
				userSession.Activate();
				_userSessionRepository.Update(userSession);
			}

			return userSession.GenerateToken(_utcTimeProvider.UtcNow);
		}

		public AccessToken SignIn(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			var user = _userRepository.GetByEmail(email);
			if(user == null)
			{
				throw new UserNotFoundException(email);
			}

			user.SignIn(password);

			var session = _userSessionRepository.GetLastSession(user.Id);
			if(session == null)
			{
				throw new UserSessionNotStartedException(user.Email);
			}

			session.Activate();
			_userSessionRepository.Update(session);

			return session.GenerateToken(_utcTimeProvider.UtcNow);
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
