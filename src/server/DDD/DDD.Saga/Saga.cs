using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Saga
{
	public class Saga : AEventSourcing<SagaId, ISagaEvent>
	{
		private readonly List<ISagaEvent> _sagaEvents =
			new List<ISagaEvent>();

		public SagaStatus Status { get; private set; }

		public Saga(SagaId id) : base(id)
		{
		}

		public Saga(SagaId id, int initialVersion, IEnumerable<ISagaEvent> events)
			: base(id, initialVersion, events)
		{
		}

		public void Handle(ISagaEvent @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));

			Mutate(@event);
		}

		protected override void When(ISagaEvent @event)
		{
			_sagaEvents.Add(@event);
			Status = @event.Status;
		}
	}
}
