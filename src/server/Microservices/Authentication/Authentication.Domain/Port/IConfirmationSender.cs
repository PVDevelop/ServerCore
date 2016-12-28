using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Model.Confirmation;

namespace PVDevelop.UCoach.Domain.Port
{
	public interface IConfirmationSender
	{
		void Send(ConfirmationKey confirmationKey);
	}
}
