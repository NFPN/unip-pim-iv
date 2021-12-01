using BlackRiver.EntityModels;
using System.Linq;
using System.Threading.Tasks;

namespace BlackRiver.Data.Services
{
    public class UserService : IUserService
    {
        public async Task<UserLogin> Authenticate(string username, string password)
        {
            var user = (await DataServices.UserLoginService.GetAll()).FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
                return null;

            return user.WithoutPassword();
        }

        public async Task<bool> Update(string username, string password)
        {
            var user = (await DataServices.UserLoginService.GetAll()).FirstOrDefault(x => x.Username == username);

            if (user == null)
                return false;

            user.Password = password;

            var newLogin = await DataServices.UserLoginService.Update(user.Id, user);

            if (newLogin == null)
                return false;

            return true;
        }

        public async Task<UserLogin> GetAuthUser(string name)
        {
            return (await DataServices.UserLoginService
                .GetAll())
                .FirstOrDefault(x => x.Username == name);
        }
    }
}
