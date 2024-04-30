using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVerkoop.Repositories.Interfaces
{
    public interface IBasketDAO<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllByBestellingId(int id);
        Task<IEnumerable<int>> AddListAndGetIDs(IEnumerable<T> entityList);
    
    }
}
