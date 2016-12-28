using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Port
{
	public interface IUserRepository
	{
		void SaveUser(User user);

		User GetUserById(UserId userId);

		User GetUserByEmail(string email);
	}
}
