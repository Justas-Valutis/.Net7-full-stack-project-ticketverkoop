using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVerkoop.Repositories.Interfaces
{
    public interface IRingDAO<T> where T : class
    {
        Task<IEnumerable<T>?> GetRingsByStadiumId(int Id);
        Task<int> GetStadiumCapacity(int StadiumId);

    }
}
