using System.Collections.Generic;
using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Port
{
	public interface IUserSessionRepository
	{
		void SaveSession(UserSession session);

		IReadOnlyCollection<UserSession> GetSessions(UserId userId);
	}
}
