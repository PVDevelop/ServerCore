namespace PVDevelop.UCoach.Authentication.Domain.Model
{
    public interface IUserRepository
    {
        /// <summary>
        /// Добавление пользователя в бд, если пользователь уже создан кидает исключение
        /// </summary>
        void Insert(User user);

        /// <summary>
        /// Поиск пользователя по Id
        /// </summary>
        /// <returns></returns>
        User FindById(string id);

        /// <summary>
        /// Поиск пользователя по email
        /// </summary>
        /// <returns></returns>
        User FindByEmail(string email);

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        void Update(User user);
    }
}
