using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Services;

public class TicketService : IBasketService<Ticket>
{
    private IBasketDAO<Ticket> basketDAO;

    public TicketService(IBasketDAO<Ticket> basketDAO)
    {
        this.basketDAO = basketDAO;
    }
    public async Task<List<int>> AddListAndGetIDs(IEnumerable<Ticket> entityList)
    {
        return await basketDAO.AddListAndGetIDs(entityList);
    }

    public async Task<IEnumerable<Ticket>?> GetAllByBestellingId(int id)
    {
        return await basketDAO.GetAllByBestellingId(id);
    }
}
