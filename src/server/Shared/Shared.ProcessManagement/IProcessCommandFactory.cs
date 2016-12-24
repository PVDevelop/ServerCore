namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public interface IProcessCommandFactory
	{
		/// <summary>
		/// Создает стартовую команду, запускающую процесс.
		/// </summary>
		IProcessCommand CreateStartCommand(ProcessId processId, ProcessStateDescription description);

		/// <summary>
		/// Создает продолженную комманду - результат выполнения по событи.
		/// </summary>
		IProcessCommand CreateContinuedCommand(IProcessEvent @event);
	}
}
