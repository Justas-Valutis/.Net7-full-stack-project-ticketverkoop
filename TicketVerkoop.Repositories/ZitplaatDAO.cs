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

    public async Task DeleteZitplaats(int sectionId, int zitPlaatsId)
    {
        Zitplaat zitplaat = await _dbContext.Zitplaats
            .Where(x => x.SectionId == sectionId && x.ZitplaatsId == zitPlaatsId
                    && x.Ticket.Match.Datum > DateTime.Now
                    && (x.Ticket.Match.Datum - DateTime.Now.AddDays(7)).TotalDays > 7)
            .FirstOrDefaultAsync();

        if (zitplaat != null)
        {
            zitplaat.ZitplaatsId = zitplaat.ZitplaatsId - 5000;
            _dbContext.Entry(zitplaat).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }

    public async Task<List<int>> ReserveerStoelen(IEnumerable<Zitplaat> stoelen)
    {
        var listStoelennID = new List<int>();
        foreach (var item in stoelen)
        {
            int aantalZitplaatsen = await _dbContext.Sections
                .Where(s => s.SectionId == item.SectionId)
                .Select(s => s.AantalZitplaatsen)
                .FirstOrDefaultAsync();
            int a = 1;
            bool itemAdded = false;
            while (a <= aantalZitplaatsen && !itemAdded)
            {
                item.ZitplaatsId = a;
                try
                {
                    _dbContext.Add(item).State = EntityState.Added;
                    await _dbContext.SaveChangesAsync();
                    listStoelennID.Add(item.ZitplaatsId);
                    itemAdded = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    a++;
                }
            }
            if (!itemAdded)
            {
                throw new Exception("Alle zitplaatsen zijn bezet op je gekozen section en ring");
            }
        }
        return listStoelennID;
    }
}
