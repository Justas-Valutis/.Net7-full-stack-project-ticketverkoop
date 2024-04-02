using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVerkoop.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>?> GetAll();
        Task<T?> FindById(int Id);
        Task AddA(T entity);
        Task Delete(T entity);

        Task Update (T entity);
        Task<T?> Get (int v);
    }
}
