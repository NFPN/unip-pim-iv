using BlackRiver.EntityModels;
using System.Threading.Tasks;

namespace BlackRiver.Data.Services
{
    public interface IUserService
    {
        Task<UserLogin> Authenticate(string username, string password);
    }
}
