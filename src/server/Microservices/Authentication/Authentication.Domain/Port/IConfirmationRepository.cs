using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Model.Confirmation;

namespace PVDevelop.UCoach.Domain.Port
{
	public interface IConfirmationRepository
	{
		void SaveConfirmation(ConfirmationAggregate confirmation);

		ConfirmationAggregate GetConfirmation(ConfirmationKey confirmationKey);
	}
}
