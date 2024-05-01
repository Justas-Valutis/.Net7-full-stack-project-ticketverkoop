using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Services;

public class ZitplaatService : IStoelService<Zitplaat>
{
    private readonly IStoelDAO<Zitplaat> stoelDAO;

    public ZitplaatService(IStoelDAO<Zitplaat> stoelDAO)
    {
        this.stoelDAO = stoelDAO;
    }
    public async Task< List<int>> ReserveerStoelen(IEnumerable<Zitplaat> stoelen)
    {
       return await stoelDAO.ReserveerStoelen(stoelen);
    }
}
