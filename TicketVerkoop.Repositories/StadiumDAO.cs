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

    public Task<Stadium?> FindById(int Id)
    {
        throw new NotImplementedException();
    }

    public Task Add(Stadium entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Stadium entity)
    {
        throw new NotImplementedException();
    }


    public Task<IEnumerable<Stadium>?> GetMatchByStadiumId(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Stadium>?> GetMatchByPloegId(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Stadium>?> GetMatchByPloegId(int PlegId, int StadiumId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Stadium>?> GetMatchByPloegIdAndStadiumId(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Stadium>?> GetMatchByPloegIdAndStadiumId(int PlegId, int StadiumId)
    {
        throw new NotImplementedException();
    }
}
