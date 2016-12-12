using System;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.UserAggregate;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port;
using PVDevelop.UCoach.Infrastructure;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.InMemory
{
	public sealed class InMemoryUserRepositiry : IUserRepository2
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IEventSourcedAggregateRepository _aggregateRepository;
		private readonly IUserConstraintRepository _userConstraintRepository;

		public InMemoryUserRepositiry(
			IUnitOfWork unitOfWork,
			IEventSourcedAggregateRepository aggregateRepository,
			IUserConstraintRepository userConstraintRepository)
		{
			if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
			if (aggregateRepository == null) throw new ArgumentNullException(nameof(aggregateRepository));
			if (userConstraintRepository == null) throw new ArgumentNullException(nameof(userConstraintRepository));

			_unitOfWork = unitOfWork;
			_aggregateRepository = aggregateRepository;
			_userConstraintRepository = userConstraintRepository;
		}

		public void AddUpdate(User user)
		{
			using (var transaction = _unitOfWork.BeginTransaction())
			{
				_userConstraintRepository.Add(user);
				_aggregateRepository.SaveAggregate(user);

				transaction.Commit();
			}
		}

		public User GetById(Guid id)
		{
			return _aggregateRepository.RestoreAggregate(id, User.Restore);
		}

		public User GetByEmail(string email)
		{
			var userId = _userConstraintRepository.GetUserId(email);
			return _aggregateRepository.RestoreAggregate(userId, User.Restore);
		}
	}
}
