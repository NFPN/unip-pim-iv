using BlackRiver.EntityModels;
using System.Linq;
using System.Threading.Tasks;

namespace BlackRiver.Data.Services
{
    public class UserService : IUserService
    {
        public readonly GenericDataService<UserLogin> loginService;
        public readonly GenericDataService<Funcionario> funcionarioService;
        public readonly GenericDataService<Hospede> hospedeService;

        public UserService()
        {
            loginService = new GenericDataService<UserLogin>(new BlackRiverDBContextFactory());
            funcionarioService = new GenericDataService<Funcionario>(new BlackRiverDBContextFactory());
            hospedeService = new GenericDataService<Hospede>(new BlackRiverDBContextFactory());
        }

        public async Task<UserLogin> Authenticate(string username, string password)
        {
            var user = (await loginService.GetAll()).FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
                return null;

            return user.WithoutPassword();
        }

        public async Task<bool> Register(UserLogin login, int id, bool isCustomer)
        {
            var user = (await loginService.GetAll()).FirstOrDefault(l => l.Username.Equals(login.Username));

            if (user != null)
                return false;

            _ = await loginService.Create(login);

            if (isCustomer)
            {
                var hospede = (await hospedeService.GetAll()).FirstOrDefault(h => h.Email == login.Username);

                if (hospede != null)
                {
                    hospede.Login = login;
                    _ = await hospedeService.Update(id, hospede);
                }
            }
            else
            {
                var funcionario = (await funcionarioService.GetAll()).FirstOrDefault(h => h.Email == login.Username);

                if (funcionario != null)
                {
                    funcionario.Login = login;
                    _ = await funcionarioService.Update(id, funcionario);
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
