using System;

namespace PVDevelop.UCoach.Domain.Exceptions
{
	/// <summary>
	/// Генерируется при попытке выполнения действий пользователя, связанных с аутентификацией в ситуации, когда сессия пользователя еще не начата.
	/// </summary>
	public class UserSessionNotStartedException : Exception
	{
		public UserSessionNotStartedException(string userName) :
			base($"User '{userName}' session has not been started yet.")
		{
		}
	}
}
