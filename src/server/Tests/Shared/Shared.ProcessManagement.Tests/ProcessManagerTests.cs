using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Shared.ProcessManagement.Tests
{
	[TestFixture]
	public class ProcessManagerTests
	{
		[Test]
		public void ExecuteProcess_WalksThroughTheProcessTillTheEnd()
		{
			var eventStore = new EventStore.EventStore();

			var eventSourcingRepository = new EventSourcingRepository(eventStore);

			var repository = new ProcessRepository(eventSourcingRepository);

			ProcessManager processManager = null;

			processManager = new ProcessManager(
				repository,
				new TestProcessCommandExecutor(() => processManager),
				new TestProcessCommandFactory());

			var processId = processManager.StartProcess(GetProcessStateDescriptions().ToList());

			Thread.Sleep(TimeSpan.FromSeconds(2));

			var process = repository.GetProcess(processId);

			Assert.AreEqual(ProcessStatus.Success, process.Status);
		}

		private static IEnumerable<ProcessStateDescription> GetProcessStateDescriptions()
		{
			yield return ProcessStateDescription.Start<CreateUser>();
			yield return ProcessStateDescription.Continue<UserCreated, CreateConfirmation>();
			yield return ProcessStateDescription.Complete<ConfirmationCreated>();
		}

		private class TestProcessCommandExecutor : IProcessCommandExecutor
		{
			private readonly Func<ProcessManager> _processManager;

			public TestProcessCommandExecutor(Func<ProcessManager> processManager)
			{
				_processManager = processManager;
			}

			public void Execute(IProcessCommand command)
			{
				Task.Run(new Action(() => Execute((dynamic) command)));
			}

			private void Execute(CreateUser command)
			{
				_processManager().HandleEvent(new UserCreated(command.ProcessId));
			}

			private void Execute(CreateConfirmation command)
			{
				_processManager().HandleEvent(new ConfirmationCreated(command.ProcessId));
			}

			private void Execute(object command)
			{
				throw new InvalidOperationException();
			}
		}

		private class TestProcessCommandFactory : IProcessCommandFactory
		{
			public IProcessCommand CreateStartCommand(ProcessId processId, ProcessStateDescription description)
			{
				return new CreateUser(processId);
			}

			public IProcessCommand CreateContinuedCommand(IProcessEvent @event)
			{
				return Create((dynamic) @event);
			}

			private IProcessCommand Create(UserCreated @event)
			{
				return new CreateConfirmation(@event.ProcessId);
			}

			private IProcessCommand Create(object @event)
			{
				throw new InvalidOperationException();
			}
		}

		private class CreateUser : AProcessCommand
		{
			public CreateUser(ProcessId processId) : base(processId)
			{
			}
		}

		private class CreateConfirmation : AProcessCommand
		{
			public CreateConfirmation(ProcessId processId) : base(processId)
			{
			}
		}

		private class UserCreated : AProcessEvent
		{
			public UserCreated(ProcessId processId) : base(processId)
			{
			}
		}

		private class ConfirmationCreated : AProcessEvent
		{
			public ConfirmationCreated(ProcessId processId) : base(processId)
			{
			}
		}
	}
}
