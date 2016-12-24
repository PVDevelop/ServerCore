using System;

namespace PVDevelop.UCoach.Shared.ProcessManagement.Events
{
	public class EventAdded : IProcessSourcingEvent
	{
		public IProcessEvent Event { get; }

		public EventAdded(IProcessEvent @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));

			Event = @event;
		}
	}
}
