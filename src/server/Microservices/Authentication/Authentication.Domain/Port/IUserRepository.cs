using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Port
{
	public interface IUserRepository
	{
		void AddUser(User user);
	}
}
