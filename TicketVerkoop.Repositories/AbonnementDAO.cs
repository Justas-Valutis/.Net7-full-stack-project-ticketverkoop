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

    public async Task<IEnumerable<int>> AddListAndGetIDs(IEnumerable<Abonnement> entityList)
    {
        var listAbonnementenId = new List<int>();
        foreach (var item in entityList)
        {
            _dbContext.Add(item).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
                listAbonnementenId.Add(item.AbonnementId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("ERROR IN DAO" + ex.Message);
            }
        }
        return listAbonnementenId;
    }

    public Task<IEnumerable<Abonnement>?> GetAllByBestellingId(int id)
    {
        throw new NotImplementedException();
    }
}
