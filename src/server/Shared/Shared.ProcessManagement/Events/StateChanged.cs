using System;

namespace PVDevelop.UCoach.Shared.ProcessManagement.Events
{
	public class StateChanged : IProcessSourcingEvent
	{
		public object State { get; }

		public StateChanged(object state)
		{
			if (state == null) throw new ArgumentNullException(nameof(state));

			State = state;
		}
	}
}
