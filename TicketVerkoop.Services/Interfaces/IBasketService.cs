using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVerkoop.Services.Interfaces
{
    public interface IBasketService<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllByBestellingId(int id);
        Task<List<int>> AddListAndGetIDs(IEnumerable<T> entityList);
    }
}
