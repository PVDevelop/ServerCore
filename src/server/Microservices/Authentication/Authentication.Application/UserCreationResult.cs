namespace PVDevelop.UCoach.Application
{
	public class UserCreationResult
	{
		public UserCreationState State { get; }

		public UserCreationResult(UserCreationState state)
		{
			State = state;
		}
	}
}
