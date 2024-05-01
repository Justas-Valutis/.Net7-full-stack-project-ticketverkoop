using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Services;

public class AbonnementService : IBasketService<Abonnement>
{
    private IBasketDAO<Abonnement> basketDAO;

    public AbonnementService(IBasketDAO<Abonnement> basketDAO)
    {
        this.basketDAO = basketDAO;
    }
    public async Task<List<int>> AddListAndGetIDs(IEnumerable<Abonnement> entityList)
    {
        return await basketDAO.AddListAndGetIDs(entityList);
    }

    public Task<IEnumerable<Abonnement>?> GetAllByBestellingId(int id)
    {
        throw new NotImplementedException();
    }
}
