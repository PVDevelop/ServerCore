using System.Collections.Generic;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public interface IProcessManager : IEventObserver<IProcessEvent>
	{
		ProcessId StartProcess(List<ProcessStateDescription> processStateDescriptions);

		object GetProcessState(ProcessId processId);
	}
}
