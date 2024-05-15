using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories;

public class RingDAO : IRingDAO<Ring>
{
    private readonly SoccerDbContext _context;

    public RingDAO()
    {
        _context = new SoccerDbContext();
    }
    public async Task<IEnumerable<Ring>?> GetRingsByStadiumId(int Id)
    {
        try
        {
            return await _context.Rings
                .Where(r => r.StadiumId == Id)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO" + ex.Message);
        }
    }

    public async Task<int> GetStadiumCapacity(int StadiumId)
    {
        int stadiumCapaciteit = 0;
        try
        {
            List<Ring> rings = await _context.Rings
                .Where(r => r.StadiumId == StadiumId)
                .Include(s => s.Sections)
                .ToListAsync();

            foreach (var item in rings)
            {
                foreach (var section in item.Sections)
                {
                    stadiumCapaciteit += section.AantalZitplaatsen;
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO" + ex.Message);
        }

        return stadiumCapaciteit;
    }
}
