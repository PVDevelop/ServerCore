using System;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Domain.Model
{
	public class UserSessionId : GuidBasedIdentifier
	{
		public UserSessionId()
		{
		}

		public UserSessionId(Guid value) :
			base(value)
		{
		}
	}
}