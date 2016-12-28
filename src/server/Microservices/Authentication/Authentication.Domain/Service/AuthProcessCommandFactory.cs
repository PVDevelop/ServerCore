using System;
using PVDevelop.UCoach.Domain.Commands;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Service
{
	public class AuthProcessCommandFactory : IProcessCommandFactory
	{
		public IProcessCommand CreateCommand(IProcessEvent @event)
		{
			return DoCreateCommand((dynamic) @event);
		}

		private IProcessCommand DoCreateCommand(RegisterUserRequested @event)
		{
			return new CreateUser(@event.ProcessId, @event.Email, @event.Password);
		}

		private IProcessCommand DoCreateCommand(UserCreated @event)
		{
			return new CreateConfrimation(@event.ProcessId, @event.UserId);
		}

		private IProcessCommand DoCreateCommand(ConfirmUserRequested @event)
		{
			return new ApproveConfirmation(@event.ProcessId, @event.ConfirmationKey);
		}

		private IProcessCommand DoCreateCommand(ConfirmationApproved @event)
		{
			return new ConfirmUser(@event.ProcessId, @event.UserId);
		}

		private IProcessCommand DoCreateCommand(UserConfirmed @event)
		{
			return new StartSession(@event.ProcessId, @event.UserId);
		}

		private IProcessCommand DoCreateCommand(UserSignInRequested @event)
		{
			return new SignIn(@event.ProcessId, @event.Email, @event.Password);
		}

		private IProcessCommand DoCreateCommand(SignInApproved @event)
		{
			return new GenerateToken(@event.ProcessId, @event.UserId);
		}

		private IProcessCommand DoCreateCommand(TokenValidationRequested @event)
		{
			return new ValidateToken(@event.ProcessId, @event.Token);
		}

		private IProcessCommand DoCreateCommand(UserSignOutRequested @event)
		{
			return new SignOut(@event.ProcessId, @event.UserId);
		}

		private IProcessCommand DoCreateCommand(UserSignedOut @event)
		{
			return new DeactivateSession(@event.ProcessId, @event.UserId);
		}

		private IProcessCommand DoCreateCommand(object @event)
		{
			throw new InvalidOperationException($"Unknown event '{@event}'.");
		}
	}
}
