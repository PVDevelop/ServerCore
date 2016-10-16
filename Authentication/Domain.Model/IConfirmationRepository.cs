namespace PVDevelop.UCoach.Authentication.Domain.Model
{
	public interface IConfirmationRepository
	{
		void Replace(Confirmation confirmation);

		Confirmation FindByConfirmation(string key);

		Confirmation FindByConfirmationByUserId(string userId);
	}
}
