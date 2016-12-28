using System;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Domain.Model.User
{
	public class UserId : AGuidBasedIdentifier
	{
		public UserId(Guid value) : base(value)
		{
		}
	}
}