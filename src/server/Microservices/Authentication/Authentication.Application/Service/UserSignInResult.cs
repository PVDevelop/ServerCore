namespace PVDevelop.UCoach.Application.Service
{
	public class UserSignInResult
	{
		public UserSignInStatus Status { get; }

		public UserSignInResult(UserSignInStatus status)
		{
			Status = status;
		}
	}
}
