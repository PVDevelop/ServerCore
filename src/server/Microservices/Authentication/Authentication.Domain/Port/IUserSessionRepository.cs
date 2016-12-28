using System.Collections.Generic;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Domain.Model.UserSession;

namespace PVDevelop.UCoach.Domain.Port
{
	public interface IUserSessionRepository
	{
		void SaveSession(UserSessionAggregate session);

		IReadOnlyCollection<UserSessionAggregate> GetSessions(UserId userId);
	}
}
