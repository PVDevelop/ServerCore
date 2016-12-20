using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Service
{
	public interface IUserRepository
	{
		void SaveUser(User user);

		User GetUserById(UserId userId);
	}
}
