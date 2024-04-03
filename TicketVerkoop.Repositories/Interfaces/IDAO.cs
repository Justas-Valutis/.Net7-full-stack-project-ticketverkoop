using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVerkoop.Repositories.Interfaces
{
    public interface IDAO<T> where T : class
    {
        Task<IEnumerable<T>?> GetAll();
        Task<T?> FindById(int Id);
        Task<IEnumerable<T>?> GetMatchByStadiumId(int Id);
        Task Add(T entity);
        Task Delete(T entity);

        Task Update(T entity);
    }
}
