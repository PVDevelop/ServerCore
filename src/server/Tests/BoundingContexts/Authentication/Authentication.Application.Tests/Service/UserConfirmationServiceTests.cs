using System;
using System.Threading;
using NUnit.Framework;
using PVDevelop.UCoach.Application.Service;
using PVDevelop.UCoach.Authentication.Infrastructure.Adapter;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Shared.EventSourcing;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Application.Tests.Service
{
	[TestFixture]
	public class UserConfirmationServiceTests
	{
		[Test]
		public void ConfirmUser_UserDaoReturnsExpectedResult()
		{
			var eventStore = new EventStore.EventStore();

			var eventSourcedRepository = new EventSourcingRepository(eventStore);

			var userRepository = new UserRepository(eventSourcedRepository);

			var confirmationRepository = new ConfirmationRepository(eventSourcedRepository);

			var userSessionRepository = new UserSessionRepository(eventSourcedRepository);

			var userConfirmationDomainService = new Domain.Service.UserConfirmationService(
				userRepository,
				userSessionRepository);

			var userDao = new UserDao();

			var observable = new EventObservable();

			observable.AddObserver(userConfirmationDomainService);
			observable.AddObserver(userDao);

			using (var eventStorePuller = new EventStorePuller(eventStore, observable, TimeSpan.FromMilliseconds(100)))
			{
				eventStorePuller.Start();

				var user = new User(new UserId(Guid.NewGuid()), "some@mail.ru", "P@ssw0rd");
				userRepository.SaveUser(user);

				var confirmation = new Confirmation(new ConfirmationKey("SomeConfirmationKey"), user.Id);
				confirmationRepository.SaveConfirmation(confirmation);

				var userConfirmationAppService = new UserConfirmationService(confirmationRepository);

				userConfirmationAppService.ConfirmUser(confirmation.Id);

				Thread.Sleep(TimeSpan.FromSeconds(5));

				var status = userDao.GetUserConfirmationStatus(confirmation.Id);

				Assert.AreEqual(UserConfirmationStatus.Confirmed, status);
			}
		}
	}
}