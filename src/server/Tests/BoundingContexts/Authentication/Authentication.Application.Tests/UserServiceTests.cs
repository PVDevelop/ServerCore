using System;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using PVDevelop.UCoach.Authentication.Infrastructure;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Saga;
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

			var userCreationService = new UserCreationService(
				userRepository,
				confirmationRepository,
				confirmationKeyGenerator,
				new FakeConfirmationSender());

			var sagaRepository = new SagaRepository(eventSourcedRepository, SagaHelper.StreamIdPrefix);

			var sagaManager = new SagaManager(sagaRepository);

			var observable = new EventObservable();
			observable.AddObserver(AggregateHelper.BuildObservableFilter(), sagaManager);
			observable.AddObserver(SagaHelper.BuildObservableFilter(), userCreationService);

			using (var eventStorePuller = new EventStorePuller(eventStore, observable, TimeSpan.FromMilliseconds(100)))
			{
				eventStorePuller.Start();

				var userService = new UserService(sagaManager);

				var userDao = new UserDao(sagaManager);

				var sagaId = new SagaId(Guid.NewGuid());
				userService.CreateUser(sagaId, "some@mail.ru", "P@ssw0rd");

				Thread.Sleep(TimeSpan.FromSeconds(5));

				var result = userDao.GetUserCreationResult(sagaId);
				Assert.AreEqual(SagaStatus.Success, result);
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
		//		Assert.AreEqual(SagaStatus.Success, result);
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