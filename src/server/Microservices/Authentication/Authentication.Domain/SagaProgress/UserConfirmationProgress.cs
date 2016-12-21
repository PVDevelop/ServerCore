namespace PVDevelop.UCoach.Domain.SagaProgress
{
	public class UserConfirmationProgress
	{
		public UserConfirmationStatus Status { get; }

		public UserConfirmationProgress(UserConfirmationStatus status)
		{
			Status = status;
		}
	}
}
