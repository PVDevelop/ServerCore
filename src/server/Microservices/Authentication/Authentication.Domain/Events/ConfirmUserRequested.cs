using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class ConfirmUserRequested : AProcessEvent
	{
		public ConfirmationKey ConfirmationKey { get; }

		public ConfirmUserRequested(
			ProcessId processId,
			ConfirmationKey confirmationKey)
			: base(processId, UserConfirmationProcessState.ConfirmationRequested)
		{
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));

			ConfirmationKey = confirmationKey;
		}
	}
}
