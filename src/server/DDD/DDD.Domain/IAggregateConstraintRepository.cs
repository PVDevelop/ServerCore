namespace PVDevelop.UCoach.Domain
{
	public interface IAggregateConstraintRepository
	{
		AggregateConstraint<TAggregateId> GetConstraint<TAggregateId>(string aggregateName, AggregateConstraintId key);

		void SaveConstraint<TAggregateId>(string aggregateName, AggregateConstraint<TAggregateId> constraint);
	}
}
