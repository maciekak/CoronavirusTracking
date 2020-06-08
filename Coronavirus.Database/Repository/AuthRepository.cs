using System.Linq;
using Coronavirus.Database.Entities;

namespace Coronavirus.Database.Repository
{
    public class AuthRepository
    {
        private readonly UserRepository _userRepository = new UserRepository();

        public bool UserExists(string login, string password)
        {
            return Context.Auths.Any(a => a.Login == login && a.Password == password);
        }

        public void AddAdmin(string login, string password, string deviceId)
        {
            Context.Auths.Add(new Auth
            {
                Login = login,
                Password = password,
                UserId = Context.UserIdCounter
            });
            _userRepository.AddUser(deviceId, "", true);
        }

        public int GetUserIdByLogin(string login)
        {
            return Context.Auths.FirstOrDefault(a => a.Login == login)?.UserId ?? 0;
        }
    }
}
