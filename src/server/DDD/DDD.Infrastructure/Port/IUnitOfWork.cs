namespace PVDevelop.UCoach.Infrastructure.Port
{
	public interface IUnitOfWork
	{
		ITransaction BeginTransaction();
	}
}
