using System;
using System.Threading;

namespace PVDevelop.UCoach.Microservice
{
	public interface IMicroservice : IDisposable
	{
		void Start(CancellationToken cancellationToken);
	}
}
