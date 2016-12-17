using System;

namespace PVDevelop.UCoach.Infrastructure.Port
{
	public interface ITransaction : IDisposable
	{
		void Commit();
	}
}
