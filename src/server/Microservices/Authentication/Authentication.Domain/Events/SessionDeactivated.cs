using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class SessionDeactivated :
		AProcessEvent,
		IDomainEvent
	{
		public SessionDeactivated(ProcessId processId) 
			: base(processId, UserSignOutProcessState.SessionDeactivated)
		{
		}
	}
}
