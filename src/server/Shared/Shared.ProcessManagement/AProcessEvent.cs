using System;

namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public abstract class AProcessEvent : IProcessEvent
	{
		public ProcessId ProcessId { get; }

		protected AProcessEvent(ProcessId processId)
		{
			if (processId == null) throw new ArgumentNullException(nameof(processId));

			ProcessId = processId;
		}
	}
}
