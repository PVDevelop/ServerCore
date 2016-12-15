using System;
using System.Threading;
using NUnit.Framework;
using PVDevelop.UCoach.Infrastructure;

namespace PVDevelop.UCoach.Application.Tests
{
	[TestFixture]
	public class UserServiceTests
	{
		[Test]
		public void RegisterUser_ExecutesSaga()
		{
			var userService = new UserService();
			var userDao = new UserDao();

			var transactionId = Guid.NewGuid();
			userService.CreateUser(transactionId, "some@mail.ru", "P@ssw0rd");

			Thread.Sleep(TimeSpan.FromSeconds(1));

			var result = userDao.GetUserCreationResult();
			Assert.AreEqual(UserCreationState.Succeeded, result.State);
		}
	}
}
