using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVerkoop.Repositories.Interfaces
{
    public interface IMatchDAO<T> where T : class
    {
        Task<IEnumerable<T>?> GetAll();
        Task<IEnumerable<T>?> GetMatchByStadiumId(int Id);

        Task<IEnumerable<T>?> GetMatchByPloegId(int Id);
        Task<IEnumerable<T>?> GetMatchByPloegIdAndStadiumId(int PlegId, int StadiumId);
        Task Add(T entity);
        Task<IEnumerable<T>?> GetAllWithHistory();
        Task<T?> FindById(int Id);
    }
}
