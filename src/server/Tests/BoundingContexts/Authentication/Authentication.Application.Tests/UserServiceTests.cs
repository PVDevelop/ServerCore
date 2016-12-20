using System;
using System.Threading;
using NUnit.Framework;
using PVDevelop.UCoach.Authentication.Infrastructure;
using PVDevelop.UCoach.Domain.Messages;
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
	public class UserServiceTests
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

				var userDao = new TransactionDao(sagaRepository);

				var transacionId = Guid.NewGuid();
				userService.CreateUser(transacionId, "some@mail.ru", "P@ssw0rd");

				Thread.Sleep(TimeSpan.FromSeconds(5));

				var result = userDao.GetStatus(transacionId);
				Assert.AreEqual(TransactionStatus.Succeeded, result);
			}

		}
		[Test]
		public void ConfirmUser_UserDaoReturnsExpectedResult()
		{
			var eventStore = new EventStore.EventStore();

			var eventSourcedRepository = new EventSourcingRepository(eventStore);

			var userRepository = new UserRepository(eventSourcedRepository);

			var confirmationRepository = new ConfirmationRepository(eventSourcedRepository);

			var userConfirmationService = new UserConfirmationService(userRepository, confirmationRepository);

			var sagaRepository = new SagaRepository(eventSourcedRepository);

			var sagaMessageDispatcher = new SagaMessageDispatcher(sagaRepository);

			var observable = new EventObservable();
			observable.AddObserver(sagaMessageDispatcher);
			observable.AddObserver(userConfirmationService);

			using (var eventStorePuller = new EventStorePuller(eventStore, observable, TimeSpan.FromMilliseconds(100)))
			{
				eventStorePuller.Start();

				var confirmationTransactionId = Guid.NewGuid();
				var sagaId = new SagaId(confirmationTransactionId);

				var userId = new UserId(Guid.NewGuid());
				var userCreatedEvent = new UserCreatedEvent(sagaId, userId, "some@mail.ru", "P@ssw0rd");
				var user = new User(userCreatedEvent);

				userRepository.SaveUser(user);

				var confirmationKey = new ConfirmationKey("confirmationKey");
				var confirmationCreatedEvent = new ConfirmationCreatedEvent(sagaId, confirmationKey, userId);
				var confirmation = new Confirmation(confirmationCreatedEvent);

				confirmationRepository.SaveConfirmation(confirmation);

				var messagePublisher = new SagaMessagePublisherToEventStore(eventStore);

				var userService = new UserService(messagePublisher);

				userService.ConfirmUser(confirmationTransactionId, confirmationKey.Value);

				Thread.Sleep(TimeSpan.FromSeconds(5));

				var userDao = new TransactionDao(sagaRepository);

				var result = userDao.GetStatus(confirmationTransactionId);
				Assert.AreEqual(TransactionStatus.Succeeded, result);
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