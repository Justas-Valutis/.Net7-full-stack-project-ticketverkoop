using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories;

public class TicketDAO : IBasketDAO<Ticket>
{
    private readonly SoccerDbContext _dbContext;

    public TicketDAO()
    {
        _dbContext = new SoccerDbContext();
    }

    public async Task<List<int>> AddListAndGetIDs(IEnumerable<Ticket> entityList)
    {
        var listTicketsID = new List<int>();
        foreach (var item in entityList)
        {
            _dbContext.Add(item).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
                listTicketsID.Add(item.TicketId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("ERROR IN DAO" + ex.Message);
            }
        }
        return listTicketsID;
    }

    public async Task<IEnumerable<Ticket>?> GetAllByBestellingId(int id)
    {
        try
        {
           return await _dbContext.Tickets.Where(t => t.BestellingId == id)
                .Include(z => z.Zitplaats).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO" + ex.Message);
        }
    }
}
