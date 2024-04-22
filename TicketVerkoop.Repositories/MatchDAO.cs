using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories;

public class MatchDAO : IMatchDAO<Match>
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
            return await _dbContext.Matches.Where(d => d.Datum >= DateTime.Now)
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

    public async Task<IEnumerable<Match>?> GetMatchByStadiumId(int Id)
    {
        try
        {
            return await _dbContext.Matches.Where(s => s.StadiumId == Id && s.Datum >= DateTime.Now)
                .Include(s => s.Stadium)
                .Include(t => t.PloegThuis)
                .Include(t => t.PloegUit)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO GET MATCH BY ID" + ex.Message);
        }
    }
    public async Task<IEnumerable<Match>?> GetMatchByPloegId(int Id)
    {
        try
        {
            return await _dbContext.Matches.Where(p => p.PloegThuisId == Id || p.PloegUitId == Id && p.Datum >= DateTime.Now)
                .Include(s => s.Stadium)
                .Include(t => t.PloegThuis)
                .Include(t => t.PloegUit)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO GET MATCH BY ID" + ex.Message);
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

    public async Task<IEnumerable<Match>?> GetMatchByPloegIdAndStadiumId(int PlegId, int StadiumId)
    {
        try
        {
            return await _dbContext.Matches.Where(p => (p.PloegThuisId == PlegId || p.PloegUitId == PlegId) 
                    && p.StadiumId == StadiumId && p.Datum >= DateTime.Now)
                .Include(s => s.Stadium)
                .Include(t => t.PloegThuis)
                .Include(t => t.PloegUit)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO GET MATCH BY ID" + ex.Message);
        }
    }

    public async Task<IEnumerable<Match>?> GetAllWithHistory()
    {
        try
        {
            return await _dbContext.Matches.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO" + ex.Message);
        }
    }

    public async Task<Match?> FindById(int Id)
    {
        try
        {
            return await _dbContext.Matches.Where(m => m.MatchId == Id)
                .Include(t => t.PloegThuis)
                .Include(t => t.PloegUit)
                .Include(s => s.Stadium)
                .ThenInclude(s => s.Rings)
                .ThenInclude(r => r.Sections)
                .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO GET MATCH BY ID" + ex.Message);
        }
    }
}
