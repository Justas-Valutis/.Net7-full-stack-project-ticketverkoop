using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVerkoop.Services.Interfaces
{
    public interface IStoelService<T> where T : class
    {
        Task ReserveerStoelen(IEnumerable<T> stoelen);
    }
}
