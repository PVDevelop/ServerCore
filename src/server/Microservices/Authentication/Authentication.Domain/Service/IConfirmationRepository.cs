using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Service
{
	public interface IConfirmationRepository
	{
		void SaveConfirmation(Confirmation confirmation);

		Confirmation GetConfirmation(ConfirmationKey confirmationKey);
	}
}
