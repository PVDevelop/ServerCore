namespace PVDevelop.UCoach.Shared.ProcessManagement.Events
{
	public class StatusChanged : IProcessSourcingEvent
	{
		public ProcessStatus Status { get; }

		public StatusChanged(ProcessStatus status)
		{
			Status = status;
		}
	}
}
