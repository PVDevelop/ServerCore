using System;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public class ProcessRepository : IProcessRepository
	{
		public const string StreamIdPrefix = "Process";

		private readonly IEventSourcingRepository _eventSourcingRepository;

		public ProcessRepository(IEventSourcingRepository eventSourcingRepository)
		{
			_eventSourcingRepository = eventSourcingRepository;

			if (eventSourcingRepository == null) throw new ArgumentNullException(nameof(eventSourcingRepository));
		}

		public Process GetProcess(ProcessId processId)
		{
			return _eventSourcingRepository.RestoreEventSourcing<ProcessId, IProcessSourcingEvent, Process>(
				StreamIdPrefix,
				processId,
				(id, version, events) => new Process(id, version, events));
		}

		public void SaveProcess(Process process)
		{
			_eventSourcingRepository.SaveEventSourcing(StreamIdPrefix, process);
		}
	}
}
