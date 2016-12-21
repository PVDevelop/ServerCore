using System;
using System.Threading;
using NUnit.Framework;
using PVDevelop.UCoach.Authentication.Infrastructure;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Shared.EventSourcing;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Application.Tests
{
	[TestFixture]
	public class UserServiceTests
	{
		[Test]
		public void RegisterUser_UserDaoReturnsExpectedResult()
		{
			var eventStore = new EventStore.EventStore();

			var eventSourcedRepository = new EventSourcingRepository(eventStore);

			var userRepository = new UserRepository(eventSourcedRepository, AggregateHelper.GetAggregateStreamIdPrefix("User"));

			var confirmationRepository = new ConfirmationRepository(eventSourcedRepository, AggregateHelper.GetAggregateStreamIdPrefix("Confirmation"));

			var confirmationKeyGenerator = new ConfirmationKeyGenerator();

			var userCreationService = new UserRegistrationService(
				userRepository,
				confirmationRepository,
				confirmationKeyGenerator,
				new FakeConfirmationSender());

			var userDao = new UserDao();

			var observable = new EventObservable();

			observable.AddObserver(userCreationService, new ObservableFilter(null));
			observable.AddObserver(userDao, new ObservableFilter(null));

			using (var eventStorePuller = new EventStorePuller(eventStore, observable, TimeSpan.FromMilliseconds(100)))
			{
				eventStorePuller.Start();

				var userService = new UserService(userCreationService);

				var userId = new UserId(Guid.NewGuid());
				userService.CreateUser(userId, "some@mail.ru", "P@ssw0rd");

				Thread.Sleep(TimeSpan.FromSeconds(5));

				var status = userDao.GetUserCreationStatus(userId);

				Assert.AreEqual(UserRegistrationStatus.Registered, status);
			}
		}

		//[Test]
		//public void ConfirmUser_UserDaoReturnsExpectedResult()
		//{
		//	var eventStore = new EventStore.EventStore();

		//	var eventSourcedRepository = new EventSourcingRepository(eventStore);

		//	var userRepository = new UserRepository(eventSourcedRepository, AggregateHelper.GetAggregateStreamIdPrefix("User"));

		//	var confirmationRepository = new ConfirmationRepository(eventSourcedRepository, AggregateHelper.GetAggregateStreamIdPrefix("Confirmation"));

		//	var userConfirmationService = new UserConfirmationService(userRepository, confirmationRepository);

		//	var sagaRepository = new SagaRepository(eventSourcedRepository, SagaHelper.StreamIdPrefix);

		//	var sagaManager = new SagaManager(sagaRepository);

		//	var observable = new EventObservable();
		//	observable.AddObserver(AggregateHelper.BuildObservableFilter(), sagaManager);
		//	observable.AddObserver(SagaHelper.BuildObservableFilter(), userConfirmationService);

		//	using (var eventStorePuller = new EventStorePuller(eventStore, observable, TimeSpan.FromMilliseconds(100)))
		//	{
		//		eventStorePuller.Start();

		//		var sagaId = new SagaId(Guid.NewGuid());

		//		var userId = new UserId(Guid.NewGuid());
		//		var user = new User(sagaId, userId, "some@mail.ru", "P@ssw0rd");

		//		userRepository.SaveUser(user);

		//		var confirmationKey = new ConfirmationKey("TestConfirmationKey");
		//		var confirmation = new Confirmation(sagaId, confirmationKey, userId);

		//		confirmationRepository.SaveConfirmation(confirmation);

		//		var userService = new UserService(sagaManager);

		//		userService.ConfirmUser(sagaId, confirmationKey);

		//		Thread.Sleep(TimeSpan.FromSeconds(5));

		//		var userDao = new UserDao(sagaManager);

		//		var result = userDao.GetUserConfirmationResult(sagaId);
		//		Assert.AreEqual(SagaStatus.Registered, result);
		//	}
		//}

		private class FakeConfirmationSender : IConfirmationSender
		{
			public void Send(ConfirmationKey confirmationKey)
			{
			}
		}
	}
}