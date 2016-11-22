using System;
using NUnit.Framework;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions;

namespace PVDevelop.UCoach.AuthenticationApp.Tests.Domain.Model
{
	[TestFixture]
	public class UserTests
	{
		[Test]
		public void Ctor_NewUser_WaitingForConfirm()
		{
			var user = new User("some@mail.ru", "P@ssw0rd", DateTime.UtcNow);

			Assert.AreEqual(UserState.WaitingForCreationConfirm, user.State);
		}

		[Test]
		public void Confirm_NewUser_BecomesSignedOut()
		{
			var user = new User("some@mail.ru", "P@ssw0rd", DateTime.UtcNow);
			user.Confirm();

			Assert.AreEqual(UserState.SignedOut, user.State);
		}
		[Test]
		public void SignIn_InvalidPassword_ThrowsException()
		{
			var user = new User("some@mail.ru", "P@ssw0rd", DateTime.UtcNow);
			user.Confirm();
			Assert.Throws<InvalidPasswordException>(() => user.SignIn("invalid_password"));
		}

		[Test]
		public void SignIn_UserIsWaitingForCreationConfirm_ThrowsException()
		{
			var user = new User("some@mail.ru", "P@ssw0rd", DateTime.UtcNow);
			Assert.Throws<UserWaitingForCreationConfirmException>(() => user.SignIn("P@ssw0rd"));
		}

		[Test]
		public void SignIn_ValidPassword_BecomesSignedIn()
		{
			var user = new User("some@mail.ru", "P@ssw0rd", DateTime.UtcNow);
			user.Confirm();

			user.SignOut();
			user.SignIn("P@ssw0rd");

			Assert.AreEqual(UserState.SignedIn, user.State);
		}

		[Test]
		public void SignOut_UserIsSignedIn_BecomesSignedOut()
		{
			var user = new User("some@mail.ru", "P@ssw0rd", DateTime.UtcNow);
			user.Confirm();

			user.SignOut();
			user.SignIn("P@ssw0rd");
			user.SignOut();

			Assert.AreEqual(UserState.SignedOut, user.State);
		}
	}
}
