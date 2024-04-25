using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVerkoop.Services.Interfaces
{
    public interface IRingService<T> where T : class
    {
        Task <IEnumerable<T>> GetRingsByStadiumId(int Id);
    }
}
