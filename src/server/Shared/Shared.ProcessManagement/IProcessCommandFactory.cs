namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public interface IProcessCommandFactory
	{
		/// <summary>
		/// Создает продолженную комманду - результат выполнения по событи.
		/// </summary>
		IProcessCommand CreateCommand(IProcessEvent @event);
	}
}
