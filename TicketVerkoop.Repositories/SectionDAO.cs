using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories;

public class SectionDAO : IGetAllByDAO<Section>
{
    private readonly SoccerDbContext _dbContext;

    public SectionDAO()
    {
        _dbContext = new SoccerDbContext();
    }

    public async Task<IEnumerable<Section>?> GetAllBy(int Id)
    {
        try
        {
            return await _dbContext.Sections.Where(s => s.SectionId == Id)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO" + ex.Message);
        }
    }
}
