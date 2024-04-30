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
    public async Task ReserveerStoelen(IEnumerable<Zitplaat> stoelen)
    {
        foreach (var item in stoelen)
        {
            _dbContext.Add(item).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("ERROR IN DAO" + ex.Message);
            }
        }
    }
}
