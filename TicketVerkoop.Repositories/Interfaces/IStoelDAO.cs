using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVerkoop.Repositories.Interfaces
{
    public interface IStoelDAO<T> where T : class
    {
        Task<List<int>> ReserveerStoelen(IEnumerable<T> stoelen);
    }
}
