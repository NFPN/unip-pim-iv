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

        public async Task<bool> Register(UserLogin login, int id, bool isCustomer)
        {
            var user = (await DataServices.UserLoginService.GetAll()).FirstOrDefault(l => l.Username.Equals(login.Username));

            if (user != null)
                return false;

            _ = await DataServices.UserLoginService.Create(login);

            if (isCustomer)
            {
                var hospede = (await DataServices.HospedeService.GetAll()).FirstOrDefault(h => h.Email == login.Username);

                if (hospede != null)
                {
                    hospede.Login = login;
                    _ = await DataServices.HospedeService.Update(id, hospede);
                }
            }
            else
            {
                var funcionario = (await DataServices.FuncionarioService.GetAll()).FirstOrDefault(h => h.Email == login.Username);

                if (funcionario != null)
                {
                    funcionario.Login = login;
                    _ = await DataServices.FuncionarioService.Update(id, funcionario);
                }
            }

            return true;
        }

        public async Task<bool> Update(string username, string password)
        {
            var user = (await loginService.GetAll()).FirstOrDefault(x => x.Username == username);

            if (user == null)
                return false;

            user.Password = password;

            var newLogin = await loginService.Update(user.Id, user);

            if (newLogin == null)
                return false;

            return true;
        }

        public async Task<UserLogin> GetAuthUser(string name)
        {
            return (await loginService
                .GetAll())
                .FirstOrDefault(x => x.Username == name)
                .WithoutPassword();
        }
    }
}
