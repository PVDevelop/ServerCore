namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public interface IProcessRepository
	{
		Process GetProcess(ProcessId processId);

		void SaveProcess(Process process);
	}
}
