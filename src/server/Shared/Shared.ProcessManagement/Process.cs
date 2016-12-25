using System;
using System.Collections.Generic;
using System.Linq;
using PVDevelop.UCoach.Shared.EventSourcing;
using PVDevelop.UCoach.Shared.ProcessManagement.Events;

namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public class Process : AEventSourcing<ProcessId, IProcessSourcingEvent>, IProcess
	{
		public ProcessStatus Status { get; private set; }

		public object State { get; private set; }

		private List<ProcessStateDescription> _descriptions;

		private List<IProcessEvent> _handledEvents;

		private List<IProcessEvent> _pendingEvents;

		public Process(
			ProcessId processId,
			List<ProcessStateDescription> descriptions)
			: base(processId)
		{
			Mutate(new ProcessCreated(descriptions));
		}

		public Process(
			ProcessId id,
			int initialVersion,
			IEnumerable<IProcessSourcingEvent> events) : base(id, initialVersion, events)
		{
		}

		public void AddEvent(IProcessEvent @event)
		{
			Mutate(new EventAdded(@event));
		}

		/// <summary>
		/// Возвращает события, готовые к обработке и очищает внутреннюю коллекцию таковых.
		/// </summary>
		public IEnumerable<IProcessEvent> TakePendingEvents()
		{
			var pendingEvents = _pendingEvents.ToList();

			Mutate(new ProcessEventsTaken(pendingEvents)); // создаем копию

			var lastEvent = pendingEvents.Last();
			if (GetProcessStateDescriptionByEvent(lastEvent).IsCompletion)
			{
				Mutate(new StatusChanged(ProcessStatus.Success));
			}

			Mutate(new StateChanged(lastEvent.State));

			return pendingEvents;
		}

		public ProcessStateDescription GetStartProcessStateDescription()
		{
			return _descriptions.Single(psd => psd.EventType == null);
		}

		public ProcessStateDescription GetProcessStateDescriptionByEvent(IProcessEvent @event)
		{
			return _descriptions.Single(psd => psd.EventType == @event.GetType());
		}

		protected override void When(IProcessSourcingEvent @event)
		{
			When((dynamic) @event);
		}

		private void When(ProcessCreated @event)
		{
			_pendingEvents = new List<IProcessEvent>();
			_handledEvents = new List<IProcessEvent>();
			_descriptions = @event.Descriptions;
			Status = ProcessStatus.Pending;
		}

		private void When(EventAdded @event)
		{
			_pendingEvents.Add(@event.Event);
		}

		private void When(ProcessEventsTaken @event)
		{
			foreach (var takenEvent in @event.TakenEvents)
			{
				var removingPendingEvent = _pendingEvents.Single(e => e.GetType() == takenEvent.GetType());
				_pendingEvents.Remove(removingPendingEvent);
				_handledEvents.Add(removingPendingEvent);
			}
		}

		private void When(StatusChanged @event)
		{
			Status = @event.Status;
		}

		private void When(StateChanged @event)
		{
			State = @event.State;
		}

		private void When(object @event)
		{
			throw new InvalidOperationException();
		}
	}
}
