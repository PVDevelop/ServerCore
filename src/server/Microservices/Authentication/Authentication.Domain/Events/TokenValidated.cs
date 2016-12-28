using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class TokenValidated : 
		AProcessEvent,
		IDomainEvent
	{
		public TokenValidated(ProcessId processId)
			: base(processId, AccessTokenValidationProcessState.ValidationApproved)
		{
		}
	}
}
