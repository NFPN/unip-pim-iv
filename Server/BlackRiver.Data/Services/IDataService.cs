using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackRiver.Data.Services
{
    public interface IDataService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllData();

        Task<T> GetData(int id);

        Task<T> CreateData(T entity);

        Task<T> UpdateData(int id, T entity);

        Task<bool> DeleteData(int id);
    }
}