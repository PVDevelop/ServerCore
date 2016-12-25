using System;

namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	/// <summary>
	/// Состояние процесса, описывающее тип исполняемой команды в зависимости от типа полученного события.
	/// </summary>
	public class ProcessStateDescription
	{
		/// <summary>
		/// Создать описание состояния начала процесса.
		/// </summary>
		public static ProcessStateDescription Start<TEvent, TCommand>()
			where TEvent : IProcessEvent
			where TCommand : IProcessCommand
		{
			return new ProcessStateDescription(typeof(TEvent), typeof(TCommand));
		}

		/// <summary>
		/// Создать описание состояния продолжения процесса.
		/// </summary>
		public static ProcessStateDescription Continue<TEvent, TCommand>()
			where TEvent : IProcessEvent
			where TCommand : IProcessCommand
		{
			return new ProcessStateDescription(typeof(TEvent), typeof(TCommand));
		}

		/// <summary>
		/// Создать описание состояния окончания процеса.
		/// </summary>
		public static ProcessStateDescription Complete<TEvent>()
			where TEvent : IProcessEvent
		{
			return new ProcessStateDescription(typeof(TEvent), null);
		}

		/// <summary>
		/// Тип события, на которое реагирует менеджер процессов.
		/// </summary>
		public Type EventType { get; }

		/// <summary>
		/// Тип команды, которая отправляется в ответ на событие. Если не задан, то это конец процесса.
		/// </summary>
		public Type CommandType { get; }

		/// <summary>
		/// Возвращает true, если при этом состоянии завершается процесс.
		/// </summary>
		public bool IsCompletion => CommandType == null;

		private ProcessStateDescription(Type eventType, Type commandType)
		{
			EventType = eventType;
			CommandType = commandType;
		}
	}
}
