using System;

namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public abstract class AProcessEvent : IProcessEvent
	{
		public ProcessId ProcessId { get; }

		public object State { get; }

		protected AProcessEvent(ProcessId processId, object state)
		{
			if (processId == null) throw new ArgumentNullException(nameof(processId));
			if (state == null) throw new ArgumentNullException(nameof(state));

			ProcessId = processId;
			State = state;
		}
	}
}
