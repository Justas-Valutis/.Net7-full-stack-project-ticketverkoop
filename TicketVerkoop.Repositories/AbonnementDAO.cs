using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories;

public class AbonnementDAO : IDAO<Abonnement>
{
    private readonly SoccerDbContext _dbContext;

    public AbonnementDAO()
    {
        _dbContext = new SoccerDbContext();
    }

    //Blijkbaar find by UserID ???? Nog CODE AANPASSEN LATER
    public async Task<Abonnement?> FindById(int Id)
    {
        try
        {
            //Blijkbaar find by UserID ???? Nog CODE AANPASSEN LATER
            return await _dbContext.Abonnements.Where(a => a.AbonnementId == Id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("ERROR IN DAO" + ex.Message);
        }
    }

    public Task Add(Abonnement entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Abonnement entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Abonnement>?> GetAll()
    {
        throw new NotImplementedException();

    }

    public Task Update(Abonnement entity)
    {
        throw new NotImplementedException();
    }
}
