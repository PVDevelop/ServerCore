using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Domain.Commands;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Service
{
	public static class AuthProcessStateDescriptionFactory
	{
		public static IEnumerable<ProcessStateDescription> GetUserRegistrationProcessStateDescriptions()
		{
			yield return ProcessStateDescription.Start<RegisterUserRequested, CreateUser>();
			yield return ProcessStateDescription.Continue<UserCreated, CreateConfrimation>();
			yield return ProcessStateDescription.Complete<ConfirmationCreated>();
		}

		public static IEnumerable<ProcessStateDescription> GetUserConfirmationProcessStateDescriptions()
		{
			yield return ProcessStateDescription.Start<ConfirmUserRequested, ApproveConfirmation>();
			yield return ProcessStateDescription.Continue<ConfirmationApproved, ConfirmUser>();
			yield return ProcessStateDescription.Continue<UserConfirmed, StartSession>();
			yield return ProcessStateDescription.Complete<SessionStarted>();
		}

		public static IEnumerable<ProcessStateDescription> GetUserSignInProcessStateDescriptions()
		{
			yield return ProcessStateDescription.Start<UserSignInRequested, SignIn>();
			yield return ProcessStateDescription.Continue<SignInApproved, GenerateToken>();
			yield return ProcessStateDescription.Complete<TokenGenerated>();
		}

		public static IEnumerable<ProcessStateDescription> GetUserSignOutProcessStateDescriptions()
		{
			yield return ProcessStateDescription.Start<UserSignOutRequested, SignOut>();
			yield return ProcessStateDescription.Continue<UserSignedOut, DeactivateSession>();
			yield return ProcessStateDescription.Complete<SessionDeactivated>();
		}

		public static IEnumerable<ProcessStateDescription> GetTokenValidationStateDescriptions()
		{
			yield return ProcessStateDescription.Start<TokenValidationRequested, ValidateToken>();
			yield return ProcessStateDescription.Complete<TokenValidated>();
		}
	}
}
