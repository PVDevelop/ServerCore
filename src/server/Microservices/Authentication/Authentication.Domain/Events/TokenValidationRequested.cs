using System;
using PVDevelop.UCoach.Domain.Model.UserSession;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class TokenValidationRequested : AProcessEvent
	{
		public UserAccessToken Token { get; }

		public TokenValidationRequested(
			ProcessId processId,
			UserAccessToken token) 
			: base(processId, AccessTokenValidationProcessState.ValidationRequesed)
		{
			if (token == null) throw new ArgumentNullException(nameof(token));

			Token = token;
		}
	}
}
