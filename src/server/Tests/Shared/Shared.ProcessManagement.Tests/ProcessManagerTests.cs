using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Polly;
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
			processManager.HandleEvent(new RegisterUserRequested(processId));

			var processStatus = Policy<ProcessStatus>.
				HandleResult(ps => ps != ProcessStatus.Success).
				WaitAndRetry(20, i => TimeSpan.FromMilliseconds(100)).
				Execute(() => repository.GetProcess(processId).Status);

			Assert.AreEqual(ProcessStatus.Success, processStatus);
		}

		private static IEnumerable<ProcessStateDescription> GetProcessStateDescriptions()
		{
			yield return ProcessStateDescription.Start<RegisterUserRequested, CreateUser>();
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

			private void Exexute(RegisterUserRequested command)
			{
				_processManager().HandleEvent(new RegisterUserRequested(command.ProcessId));
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
			public IProcessCommand CreateCommand(IProcessEvent @event)
			{
				return Create((dynamic) @event);
			}

			private IProcessCommand Create(RegisterUserRequested @event)
			{
				return new CreateUser(@event.ProcessId);
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

		private class RegisterUserRequested : AProcessEvent
		{
			public RegisterUserRequested(ProcessId processId)
				: base(processId, new object())
			{
			}
		}

		private class UserCreated : AProcessEvent
		{
			public UserCreated(ProcessId processId) : base(processId, new object())
			{
			}
		}

		private class ConfirmationCreated : AProcessEvent
		{
			public ConfirmationCreated(ProcessId processId) : base(processId, new object())
			{
			}
		}
	}
}
