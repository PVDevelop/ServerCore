namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public interface IProcessEvent
	{
		/// <summary>
		/// Идентификатор выполняемого процесса.
		/// </summary>
		ProcessId ProcessId { get; }

		/// <summary>
		/// Состояние процесса. Это может быть как enum (state-machine), так и сложный объект. Но не может быть null.
		/// </summary>
		object State { get; }
	}
}
