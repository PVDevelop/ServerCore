using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Port
{
	public interface IConfirmationSender
	{
		void Send(ConfirmationKey confirmationKey);
	}
}
