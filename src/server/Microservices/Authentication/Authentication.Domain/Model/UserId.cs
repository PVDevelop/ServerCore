using System;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Domain.Model
{
	public class UserId : GuidBasedIdentifier
	{
		public UserId()
		{
		}

		public UserId(Guid value) : base(value)
		{
		}
	}
}