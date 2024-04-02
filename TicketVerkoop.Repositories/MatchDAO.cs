using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories;

public class MatchDAO : IDAO<Match>
{
    private readonly SoccerDbContext _dbContext;

    public MatchDAO()
    {
        _dbContext = new SoccerDbContext();
    }

    public async Task<IEnumerable<Match>?> GetAll()
    {
        try
        {
            return await _dbContext.Matches
                .Include(s => s.Stadium)
                .Include(t => t.PloegThuis)
                .Include(t => t.PloegUit)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO" + ex.Message);
        }
    }
    public async Task Add(Match entity)
    {
        _dbContext.Add(entity).State = EntityState.Added;
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

    public Task Delete(Match entity)
    {
        throw new NotImplementedException();
    }

    public Task<Match?> FindById(int Id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Match entity)
    {
        throw new NotImplementedException();
    }
}
