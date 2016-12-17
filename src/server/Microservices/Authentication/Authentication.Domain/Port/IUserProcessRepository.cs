using System;
using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Port
{
	/// <summary>
	/// Хранилище результатов выполнения процессов - таких, как создание пользователя, подтверждение, логон и т.д.
	/// </summary>
	public interface IUserProcessRepository
	{
		UserCreationResult GetUserCreationResult(Guid sagaId);

		void SetUserCreationResult(UserCreationResult userCreationResult);
	}
}
