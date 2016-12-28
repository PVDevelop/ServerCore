using PVDevelop.UCoach.Domain.Model.User;

namespace PVDevelop.UCoach.Domain.Port
{
	public interface IUserRepository
	{
		void SaveUser(UserAggregate user);

		UserAggregate GetUserById(UserId userId);

		UserAggregate GetUserByEmail(string email);
	}
}
