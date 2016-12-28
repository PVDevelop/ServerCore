using System;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class UserRepository : IUserRepository
	{
		public const string AggregateName = "User";
		public const string EmailKeyName = "Email";

		private readonly IEventSourcingRepository _eventSourcedAggregateRepository;
		private readonly IAggregateConstraintRepository _constraintRepository;

		public UserRepository(
			IEventSourcingRepository eventSourcedAggregateRepository,
			IAggregateConstraintRepository constraintRepository)
		{
			if (eventSourcedAggregateRepository == null) throw new ArgumentNullException(nameof(eventSourcedAggregateRepository));
			if (constraintRepository == null) throw new ArgumentNullException(nameof(constraintRepository));

			_eventSourcedAggregateRepository = eventSourcedAggregateRepository;
			_constraintRepository = constraintRepository;
		}

		public void SaveUser(UserAggregate user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));

			var constraintId = new AggregateConstraintId(EmailKeyName, user.Email);
			var constraint = new AggregateConstraint<UserId>(constraintId, user.Id);
			
			_constraintRepository.SaveConstraint(AggregateName, constraint);
			_eventSourcedAggregateRepository.SaveEventSourcing<
				UserHelper,
				UserId,
				IDomainEvent,
				UserAggregate>(user);
		}

		public UserAggregate GetUserById(UserId id)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));

			return _eventSourcedAggregateRepository.RestoreEventSourcing<
				UserHelper,
				UserId,
				IDomainEvent,
				UserAggregate>(id);
		}

		public UserAggregate GetUserByEmail(string email)
		{
			if(string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set.", nameof(email));

			var constraintKey = new AggregateConstraintId(EmailKeyName, email);
			var constraint = _constraintRepository.GetConstraint<UserId>(AggregateName, constraintKey);

			return GetUserById(constraint.AggregateId);
		}
	}
}
