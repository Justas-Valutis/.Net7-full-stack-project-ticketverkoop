using System.Numerics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Services;

public class RingService : IRingService<Ring>
{
    private IRingDAO<Ring> _ringDAO;

    public RingService(IRingDAO<Ring> ringDAO)
    {
        _ringDAO = ringDAO;
    }
    public async Task<IEnumerable<Ring>> GetRingsByStadiumId(int Id)
    {
        return await _ringDAO.GetRingsByStadiumId(Id);
    }
}
