using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackRiver.Data.Services
{
    public interface IDataService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);

        Task<T> Create(T entity);

        Task<T> Update(int id, T entity);

        Task<bool> Delete(int id);
    }
}