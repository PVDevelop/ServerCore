namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public interface IProcessCommandExecutor
	{
		void Execute(IProcessCommand command);
	}
}
