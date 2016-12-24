using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public class ProcessManager
	{
		private readonly IProcessRepository _repository;
		private readonly IProcessCommandExecutor _commandExecutor;
		private readonly IProcessCommandFactory _processCommandFactory;

		// todo: временно синхронизируем через lock, потом надо будет придумать что-то более умное (например, синхронизацию по ProcessId).
		private readonly object _sync = new object();

		public ProcessManager(
			IProcessRepository repository,
			IProcessCommandExecutor commandExecutor,
			IProcessCommandFactory processCommandFactory)
		{
			if (repository == null) throw new ArgumentNullException(nameof(repository));
			if (commandExecutor == null) throw new ArgumentNullException(nameof(commandExecutor));
			if (processCommandFactory == null) throw new ArgumentNullException(nameof(processCommandFactory));

			_repository = repository;
			_commandExecutor = commandExecutor;
			_processCommandFactory = processCommandFactory;
		}

		public ProcessId StartProcess(
			List<ProcessStateDescription> processStateDescriptions)
		{
			if (processStateDescriptions == null) throw new ArgumentNullException(nameof(processStateDescriptions));

			var processId = new ProcessId(Guid.NewGuid());
			var process = new Process(processId, processStateDescriptions);

			var processStartDescription = process.GetStartProcessStateDescription();

			var command = _processCommandFactory.CreateStartCommand(processId, processStartDescription);

			lock (_sync)
			{
				PublishCommand(command);

				_repository.SaveProcess(process);
			}

			return processId;
		}

		public void HandleEvent(
			IProcessEvent @event)
		{
			var process = _repository.GetProcess(@event.ProcessId);
			process.AddEvent(@event);

			lock (_sync)
			{
				ProcessPendingEvents(process);

				_repository.SaveProcess(process);
			}
		}

		private void ProcessPendingEvents(Process process)
		{
			foreach (var pendingEvent in process.TakePendingEvents())
			{
				var processStateDescription = process.GetProcessStateDescriptionByEvent(pendingEvent);
				if (processStateDescription.IsCompletion)
				{
					continue;
				}

				var command = _processCommandFactory.CreateContinuedCommand(pendingEvent);
				PublishCommand(command);
			}
		}

		private void PublishCommand(IProcessCommand command)
		{
			_commandExecutor.Execute(command);
		}
	}
}
