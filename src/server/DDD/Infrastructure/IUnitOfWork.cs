namespace PVDevelop.UCoach.Infrastructure
{
	public interface IUnitOfWork
	{
		ITransaction BeginTransaction();
	}
}
