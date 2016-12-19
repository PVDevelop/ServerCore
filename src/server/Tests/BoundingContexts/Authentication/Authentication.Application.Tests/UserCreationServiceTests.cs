using System;
using System.Threading;
using NUnit.Framework;
using PVDevelop.UCoach.Authentication.Infrastructure;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Infrastructure;
using PVDevelop.UCoach.Saga;
using PVDevelop.UCoach.Shared.EventSourcing;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Application.Tests
{
	[TestFixture]
	public class UserCreationServiceTests
	{
		[Test]
		public void RegisterUser_UserDaoReturnsExpectedResult()
		{
			var eventStore = new EventStore.EventStore();

			var eventSourcedRepository = new EventSourcingRepository(eventStore);

			var userRepository = new UserRepository(eventSourcedRepository);

			var confirmationRepository = new ConfirmationRepository(eventSourcedRepository);

			var confirmationKeyGenerator = new ConfirmationKeyGenerator();

			var userCreationService = new UserCreationService(
				userRepository,
				confirmationRepository,
				confirmationKeyGenerator,
				new FakeConfirmationSender());

			var sagaRepository = new SagaRepository(eventSourcedRepository);

			var sagaMessageDispatcher = new SagaMessageDispatcher(sagaRepository);

			var observable = new EventObservable();
			observable.AddObserver(sagaMessageDispatcher);
			observable.AddObserver(userCreationService);

			using (var eventStorePuller = new EventStorePuller(eventStore, observable, TimeSpan.FromMilliseconds(100)))
			{
				eventStorePuller.Start();

				var messagePublisher = new SagaMessagePublisherToEventStore(eventStore);

				var userService = new UserService(messagePublisher);

				var userDao = new UserDao(sagaRepository);

				var transacionId = Guid.NewGuid();
				userService.CreateUser(transacionId, "some@mail.ru", "P@ssw0rd");

				Thread.Sleep(TimeSpan.FromSeconds(5));

				var result = userDao.GetUserCreationResult(transacionId);
				Assert.AreEqual(UserCreationState.Succeeded, result.State);
			}
		}

		private class FakeConfirmationSender : IConfirmationSender
		{
			public void Send(ConfirmationKey confirmationKey)
			{
			}
		}
	}
}