using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Commands
{
	public class CreateConfrimation : AProcessCommand
	{
		public UserId UserId { get; }

		public CreateConfrimation(
			ProcessId processId,
			UserId userId) 
			: base(processId)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			UserId = userId;
		}
	}
}
