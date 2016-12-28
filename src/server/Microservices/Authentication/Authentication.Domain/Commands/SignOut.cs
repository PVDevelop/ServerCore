using System;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Domain.Model.UserSession;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Commands
{
	public class SignOut :
		AProcessCommand
	{
		public UserId UserId { get; }

		public SignOut(
			ProcessId processId, 
			UserId userId) : base(processId)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			UserId = userId;
		}
	}
}
