using System;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Domain
{
	public class AggregateConstraintRepository : IAggregateConstraintRepository
	{
		private readonly IEventSourcingRepository _eventSourcingRepository;

		public AggregateConstraintRepository(IEventSourcingRepository eventSourcingRepository)
		{
			if (eventSourcingRepository == null) throw new ArgumentNullException(nameof(eventSourcingRepository));

			_eventSourcingRepository = eventSourcingRepository;
		}

		public AggregateConstraint<TAggregateId> GetConstraint<TAggregateId>(
			string aggregateName,
			AggregateConstraintId id)
		{
			return _eventSourcingRepository.RestoreEventSourcing<
				AggregateConstraintHelper<TAggregateId>,
				AggregateConstraintId,
				IConstraintEvent,
				AggregateConstraint<TAggregateId>>(id);
		}

		public void SaveConstraint<TAggregateId>(
			string aggregateName,
			AggregateConstraint<TAggregateId> constraint)
		{
			_eventSourcingRepository.SaveEventSourcing<
				AggregateConstraintHelper<TAggregateId>,
				AggregateConstraintId,
				IConstraintEvent,
				AggregateConstraint<TAggregateId>>(constraint);
		}
	}
}
