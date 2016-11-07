using System;
using System.Linq.Expressions;

namespace PVDevelop.UCoach.Mongo
{
	/// <summary>
	/// Продоставляет доступ к БД MongoDB.
	/// </summary>
	public interface IMongoRepository<T>
	{
		/// <summary>
		/// Возвращает true, если объект существует, иначе - false.
		/// </summary>
		bool Contains(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Вставляет новый документ в коллекцию.
		/// </summary>
		void Insert(T document);

		/// <summary>
		/// Удаляет документ из коллекции.
		/// </summary>
		void Remove(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Находит документ по предикату. Если не найден, возвращает null.
		/// </summary>
		T Find(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Замещает имеющийся документ новым.
		/// </summary>
		void ReplaceOne(Expression<Func<T, bool>> predicate, T document);
	}
}
