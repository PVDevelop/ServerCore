using System;

namespace PVDevelop.UCoach.Infrastructure
{
	public interface ITransaction : IDisposable
	{
		void Commit();
	}
}
