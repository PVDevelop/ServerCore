using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Service
{
	public interface IUserSessionRepository
	{
		void SaveSession(UserSession session);
	}
}
