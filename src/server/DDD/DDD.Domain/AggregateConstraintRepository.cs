using System;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Domain
{
	public class AggregateConstraintRepository : IAggregateConstraintRepository
	{
		public const string StreamIdPrefix = "Aggregate.Constraint";

		private readonly IEventSourcingRepository _eventSourcingRepository;

		public AggregateConstraintRepository(IEventSourcingRepository eventSourcingRepository)
		{
			if (eventSourcingRepository == null) throw new ArgumentNullException(nameof(eventSourcingRepository));

			_eventSourcingRepository = eventSourcingRepository;
		}

		public AggregateConstraint<TAggregateId> GetConstraint<TAggregateId>(
			string aggregateName,
			AggregateConstraintId key)
		{
			return 
				_eventSourcingRepository
				.RestoreEventSourcing<AggregateConstraintId, IConstraintEvent, AggregateConstraint<TAggregateId>>(
					GetStreamIdPrefix(aggregateName),
					key,
					(k, ver, events) => new AggregateConstraint<TAggregateId>(k, ver, events));
		}

		public void SaveConstraint<TAggregateId>(
			string aggregateName,
			AggregateConstraint<TAggregateId> constraint)
		{
			_eventSourcingRepository.SaveEventSourcing(GetStreamIdPrefix(aggregateName), constraint);
		}

		private static string GetStreamIdPrefix(string aggregateName)
		{
			return $"{StreamIdPrefix}.{aggregateName}";
		}
	}
}
