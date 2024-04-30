using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories;

public class StadiumDAO : IDAO<Stadium>
{
    private readonly SoccerDbContext _dbContext;

    public StadiumDAO()
    {
        _dbContext = new SoccerDbContext();
    }
    public async Task<IEnumerable<Stadium>?> GetAll()
    {
        try
        {
            return await _dbContext.Stadia.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO" + ex.Message);
        }
    }

    public async Task<Stadium?> FindById(int Id)
    {
        try
        {
            return await _dbContext.Stadia
                  .Include(r => r.Rings)
                  .ThenInclude(s => s.Sections)
                  .ThenInclude(z => z.Zitplaats)
                  .FirstOrDefaultAsync(s => s.StadiumId == Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO" + ex.Message);
        }
    }

    public Task Add(Stadium entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Stadium entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddandGetID(Stadium entity)
    {
        throw new NotImplementedException();
    }
}
