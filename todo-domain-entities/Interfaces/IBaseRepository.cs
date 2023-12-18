using System.Collections.Generic;
using System.Threading.Tasks;

namespace todo_domain_entities.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAll();

        Task<T> GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task SaveChangesAsync();
    }
}
