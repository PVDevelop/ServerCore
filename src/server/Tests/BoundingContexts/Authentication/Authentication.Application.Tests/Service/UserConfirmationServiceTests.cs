﻿using System;
using System.Threading;
using NUnit.Framework;
using PVDevelop.UCoach.Application.Service;
using PVDevelop.UCoach.Authentication.Infrastructure.Adapter;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Application.Tests.Service
{
	[TestFixture]
	public class UserConfirmationServiceTests
	{
		[Test]
		public void ConfirmUser_UserDaoReturnsExpectedResult()
		{
			using (var authContext = new AuthenticationContextBuilder().Build())
			{
				authContext.Start();

				var createUserProcessId = new ProcessId(Guid.NewGuid());

				var user = new User(
					createUserProcessId,
					new UserId(Guid.NewGuid()),
					"some@mail.ru",
					"P@ssw0rd");

				authContext.UserRepository.SaveUser(user);

				var confirmationKey = new ConfirmationKey("SomeConfirmationKey");

				var confirmation = new Confirmation(
					createUserProcessId,
					confirmationKey,
					user.Id);

				authContext.ConfirmationRepository.SaveConfirmation(confirmation);

				var userConfirmationService = new UserConfirmationService(authContext.ProcessManager);

				var processId = userConfirmationService.ConfirmUser(confirmationKey);

				Thread.Sleep(TimeSpan.FromSeconds(5));

				var userDao = new UserDao(authContext.ProcessManager);

				var state = userDao.GetUserConfirmationState(processId);
				Assert.AreEqual(UserConfirmationProcessState.UserConfirmed, state);
			}
		}
	}
}