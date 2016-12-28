using System;
using PVDevelop.UCoach.Domain.Model.UserSession;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Commands
{
	public class ValidateToken : AProcessCommand
	{
		public UserAccessToken Token { get; }

		public ValidateToken(ProcessId processId, UserAccessToken token) : base(processId)
		{
			if (token == null) throw new ArgumentNullException(nameof(token));

			Token = token;
		}
	}
}
