using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Services;

public class AbonnementService : IBasketService<Abonnement>
{
    public Task AddList(IEnumerable<Abonnement> entityList)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Abonnement>?> GetAllByBestellingId(int id)
    {
        throw new NotImplementedException();
    }
}
