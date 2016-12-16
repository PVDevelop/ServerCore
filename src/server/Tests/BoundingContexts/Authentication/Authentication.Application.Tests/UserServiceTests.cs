using System;
using System.Threading;
using NUnit.Framework;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Infrastructure;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Application.Tests
{
	[TestFixture]
	public class UserServiceTests
	{
		[Test]
		public void RegisterUser_UserDaoReturnsExpectedResult()
		{
			var eventStore  = new InMemoryEventStore();

			var userSagaMessageConsumer = new UserSagaMessagesConsumer();

			var sagaRepository = new InMemorySagaRepository();

			var sagaMessageDispatcher = new SagaMessageDispatcher(userSagaMessageConsumer, sagaRepository);

			var sagaMessageConsumer = new EventConsumerWithSagaRedirection(eventStore, sagaMessageDispatcher);

			var messagePublisher = new SagaMessagePublisherToEventStore(eventStore);

			var userService = new UserService(messagePublisher);

			var userDao = new UserDao();

			var sagaId = Guid.NewGuid();
			userService.CreateUser(sagaId, "some@mail.ru", "P@ssw0rd");

			Thread.Sleep(TimeSpan.FromSeconds(1));

			var result = userDao.GetUserCreationResult();
			Assert.AreEqual(UserCreationState.Succeeded, result.State);
		}
	}
}
