using System;

namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public abstract class AProcessCommand : IProcessCommand
	{
		public ProcessId ProcessId { get; }

		protected AProcessCommand(ProcessId processId)
		{
			if (processId == null) throw new ArgumentNullException(nameof(processId));

			ProcessId = processId;
		}
	}
}
