using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories;

public class ZitplaatDAO : IStoelDAO<Zitplaat>
{
    private readonly SoccerDbContext _dbContext;

    public ZitplaatDAO()
    {
        _dbContext = new SoccerDbContext();
    }
    public async Task<List<int>> ReserveerStoelen(IEnumerable<Zitplaat> stoelen)
    {
        var listStoelennID = new List<int>();
        foreach (var item in stoelen)
        {
            _dbContext.Add(item).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
                listStoelennID.Add(item.ZitplaatsId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("ERROR IN DAO" + ex.Message);
            }
        }
        return listStoelennID;
    }
}
