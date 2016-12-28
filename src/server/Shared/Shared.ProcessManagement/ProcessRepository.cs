using System;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public class ProcessRepository : IProcessRepository
	{
		private readonly IEventSourcingRepository _eventSourcingRepository;

		public ProcessRepository(IEventSourcingRepository eventSourcingRepository)
		{
			_eventSourcingRepository = eventSourcingRepository;

			if (eventSourcingRepository == null) throw new ArgumentNullException(nameof(eventSourcingRepository));
		}

		public Process GetProcess(ProcessId processId)
		{
			return
				_eventSourcingRepository.RestoreEventSourcing<ProcessHelper, ProcessId, IProcessSourcingEvent, Process>(processId);
		}

		public void SaveProcess(Process process)
		{
			_eventSourcingRepository.SaveEventSourcing<ProcessHelper, ProcessId, IProcessSourcingEvent, Process>(process);
		}
	}
}
