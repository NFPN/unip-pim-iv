using BlackRiver.EntityModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackRiver.Data.Services
{
    public interface IUserService
    {
        Task<Login> Authenticate(string username, string password);

        Task<IEnumerable<Login>> GetAll();
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Login> _users = new List<Login>
        {
            new Login { Id = 1, Username = "admin", Password = "admin" }
        };

        public async Task<Login> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return user.WithoutPassword();
        }

        public async Task<IEnumerable<Login>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}
