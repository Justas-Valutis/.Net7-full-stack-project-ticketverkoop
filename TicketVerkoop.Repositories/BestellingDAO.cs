using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories;

public class BestellingDAO : IDAO<Bestelling>
{
    private readonly SoccerDbContext _dbContext;

    public BestellingDAO()
    {
        _dbContext = new SoccerDbContext();
    }
    public async Task<int> AddandGetID(Bestelling entity)
    {
        _dbContext.Add(entity).State = EntityState.Added;
        try
        {
            await _dbContext.SaveChangesAsync();
            return entity.BestellingId;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO" + ex.Message);
        }
    }

    public Task Add(Bestelling entity)
    {
        throw new NotImplementedException();

    }

    public async Task Delete(Bestelling entity)
    {
        _dbContext.Add(entity).State = EntityState.Deleted;
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

    public Task<Bestelling?> FindById(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Bestelling>?> GetAll()
    {
        try
        {
            return await _dbContext.Bestellings.Where(d => d.BestelDatum >= DateTime.Now)
               .Include(b => b.Tickets)
               .Include(b => b.Abonnements)
               .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN BestellingDAO" + ex.Message);
        }
    }

    public async Task<IEnumerable<Bestelling>?> GetAllByUserId(string UserId)
    {
        try
        {
            return await _dbContext.Bestellings
                .Where(d => d.UserId == UserId)
               .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN BestellingDAO" + ex.Message);
        }
    }
}
