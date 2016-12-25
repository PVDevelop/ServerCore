using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Commands
{
	public class ConfirmUser : AProcessCommand
	{
		public UserId UserId { get; }

		public ConfirmUser(
			ProcessId processId,
			UserId userId)
			: base(processId)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			UserId = userId;
		}
	}
}