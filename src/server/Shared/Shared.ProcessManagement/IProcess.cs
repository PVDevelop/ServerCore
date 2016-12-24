namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public interface IProcess
	{
		ProcessId Id { get; }

		ProcessStatus Status { get; }
	}
}
