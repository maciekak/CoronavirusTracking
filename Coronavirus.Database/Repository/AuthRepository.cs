using System.Linq;
using Coronavirus.Database.Entities;

namespace Coronavirus.Database.Repository
{
    public class AuthRepository
    {
        private readonly UserRepository _userRepository;
        private readonly CoronaContext _coronaContext;
        public AuthRepository(CoronaContext coronaContext)
        {
            _coronaContext = coronaContext;
            _userRepository = new UserRepository(coronaContext);
        }

        public bool UserExists(string login, string password)
        {
            return _coronaContext.Auths.Any(a => a.Login == login && a.Password == password);
        }

        public void AddAdmin(string login, string password, string deviceId)
        {
            _coronaContext.Auths.Add(new Auth
            {
                Login = login,
                Password = password
            });
            _userRepository.AddUser(deviceId, "", true);
            _coronaContext.SaveChanges();
        }

        public int GetUserIdByLogin(string login)
        {
            return _coronaContext.Auths.FirstOrDefault(a => a.Login == login)?.UserId ?? 0;
        }
    }
}
