using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Commands
{
	public class ApproveConfirmation : AProcessCommand
	{
		public ConfirmationKey ConfirmationKey { get; }

		public ApproveConfirmation(ProcessId processId, ConfirmationKey confirmationKey) 
			: base(processId)
		{
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));

			ConfirmationKey = confirmationKey;
		}
	}
}
