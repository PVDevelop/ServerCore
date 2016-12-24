using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.Shared.ProcessManagement.Events
{
	public class ProcessEventsTaken : IProcessSourcingEvent
	{
		/// <summary>
		/// Соббытия, которые были забраны
		/// </summary>
		public List<IProcessEvent> TakenEvents { get; }

		public ProcessEventsTaken(List<IProcessEvent> takenEvents)
		{
			TakenEvents = takenEvents;
		}
	}
}