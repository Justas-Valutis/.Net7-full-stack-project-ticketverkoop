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

    public async Task<List<int>> AddListAndGetIDs(IEnumerable<Abonnement> entityList)
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

    public async Task<IEnumerable<Abonnement>?> GetAllByBestellingId(int id)
    {
        try
        {
            return await _dbContext.Abonnements.Where(t => t.BestellingId == id)
                 .Include(p => p.Ploeg)
                 .ThenInclude(ploeg => ploeg.ThuisStadium)
                 .Include(s => s.Zitplaats)
                 .ThenInclude(zitplaats => zitplaats.Section)
                 .ThenInclude(section => section.Ring)
                 .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO" + ex.Message);
        }
    }
}
