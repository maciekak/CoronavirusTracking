using System.Linq;
using Coronavirus.Database.Entities;

namespace Coronavirus.Database.Repository
{
    public class AuthRepository
    {
        private readonly UserRepository _userRepository = new UserRepository();

        public bool UserExists(string login, string password)
        {
            return DatabaseTemp.Auths.Any(a => a.Login == login && a.Password == password);
        }

        public void AddAdmin(string login, string password, string deviceId)
        {
            DatabaseTemp.Auths.Add(new Auth
            {
                Login = login,
                Password = password,
                UserId = DatabaseTemp.UserIdCounter
            });
            _userRepository.AddUser(deviceId, true);
        }

        public int GetUserIdByLogin(string login)
        {
            return DatabaseTemp.Auths.FirstOrDefault(a => a.Login == login)?.UserId ?? 0;
        }
    }
}
