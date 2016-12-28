using System;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Domain.Model.UserSession
{
	public class UserSessionId : AGuidBasedIdentifier
	{
		public UserSessionId(Guid value) :
			base(value)
		{
		}
	}
}