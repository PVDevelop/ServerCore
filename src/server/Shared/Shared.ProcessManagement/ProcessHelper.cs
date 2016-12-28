using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public class ProcessHelper : IEventSourcingHelper<ProcessId, IProcessSourcingEvent, Process>
	{
		public Process CreateEventSourcing(
			ProcessId id, 
			int version, 
			IEnumerable<IProcessSourcingEvent> events)
		{
			return new Process(id, version, events);
		}

		public string GetStreamName()
		{
			return "Process";
		}

		public string GetStringId(ProcessId id)
		{
			return id.Value.ToString();
		}

		public ProcessId ParseId(string id)
		{
			return new ProcessId(Guid.Parse(id));
		}
	}
}
