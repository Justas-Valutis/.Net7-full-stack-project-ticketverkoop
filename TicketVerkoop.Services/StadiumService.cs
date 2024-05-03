using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Services;

public class StadiumService : IService<Stadium>
{
    private IDAO<Stadium> stadiumDAO;

    public StadiumService(IDAO<Stadium> _stadiumDAO)
    {
        stadiumDAO = _stadiumDAO;
    }


    public async Task<IEnumerable<Stadium>?> GetAll()
    {
        return await stadiumDAO.GetAll();
    }

    public Task Add(Stadium entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Stadium entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Stadium> FindById(int Id)
    {
        return await stadiumDAO.FindById(Id);
    }

    public Task<Stadium?> Get(int v)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddandGetID(Stadium entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Stadium>?> GetAllByUserId(string UserId)
    {
        throw new NotImplementedException();
    }
}
