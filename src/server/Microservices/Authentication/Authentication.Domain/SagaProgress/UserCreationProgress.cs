namespace PVDevelop.UCoach.Domain.SagaProgress
{
	public class UserCreationProgress
	{
		public UserCreationStatus Status { get; }

		public UserCreationProgress(UserCreationStatus status)
		{
			Status = status;
		}
	}
}
