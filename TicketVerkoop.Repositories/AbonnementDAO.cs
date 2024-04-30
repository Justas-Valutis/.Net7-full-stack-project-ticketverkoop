using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories;

public class AbonnementDAO : IBasketDAO<Abonnement>
{
    private readonly SoccerDbContext _dbContext;

    public AbonnementDAO()
    {
        _dbContext = new SoccerDbContext();
    }

    public Task AddList(IEnumerable<Abonnement> entityList)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Abonnement>?> GetAllByBestellingId(int id)
    {
        throw new NotImplementedException();
    }
}
