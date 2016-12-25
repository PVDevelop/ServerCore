using System;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Commands
{
	public class CreateUser : AProcessCommand
	{
		public string Email { get; }
		public string Password { get; }

		public CreateUser(
			ProcessId processId,
			string email,
			string password)
			: base(processId)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set.", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set.", nameof(password));

			Email = email;
			Password = password;
		}
	}
}
