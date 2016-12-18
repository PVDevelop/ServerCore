using System;
using System.Threading;
using NUnit.Framework;
using PVDevelop.UCoach.Authentication.Infrastructure;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Infrastructure.Adapter;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Application.Tests
{
	[TestFixture]
	public class UserCreationServiceTests
	{
		[Test]
		public void RegisterUser_UserDaoReturnsExpectedResult()
		{
			var eventStore = new InMemoryEventStore();

			var eventSourcedRepository = new EventSourcedAggregateRepository(eventStore);

			var userRepository = new UserRepository(eventSourcedRepository);

			var confirmationRepository = new ConfirmationRepository(eventSourcedRepository);

			var confirmationKeyGenerator = new ConfirmationKeyGenerator();

			var userProcessRepository = new InMemoryUserProcessRepository();

			var userSagaMessageConsumer = new UserCreationService(
				userRepository,
				confirmationRepository,
				confirmationKeyGenerator,
				new FakeConfirmationSender(),
				userProcessRepository);

			var sagaRepository = new InMemorySagaRepository();

			var sagaMessageDispatcher = new SagaMessageDispatcher(userSagaMessageConsumer, sagaRepository);

			var sagaMessageConsumer = new EventConsumerWithSagaRedirection(sagaMessageDispatcher);

			using (var observable = new EventStoreObservable(eventStore, TimeSpan.FromMilliseconds(100)))
			{
				observable.AddObserver(sagaMessageConsumer);
				observable.Start();

				var messagePublisher = new SagaMessagePublisherToEventStore(eventStore);

				var userService = new UserService(messagePublisher);

				var userDao = new UserDao(userProcessRepository);

				var sagaId = Guid.NewGuid();
				userService.CreateUser(sagaId, "some@mail.ru", "P@ssw0rd");

				Thread.Sleep(TimeSpan.FromSeconds(5));

				var result = userDao.GetUserCreationResult(sagaId);
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