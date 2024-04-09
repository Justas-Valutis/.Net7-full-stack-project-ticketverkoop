using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Services;

public class PloegService : IService<Ploeg>
{
    private IDAO<Ploeg> ploegDAO;

    public PloegService(IDAO<Ploeg> _ploegDAO)
    {
        ploegDAO = _ploegDAO;   
    }
    public Task Add(Ploeg entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Ploeg entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Ploeg?> FindById(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<Ploeg?> Get(int v)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Ploeg>?> GetAll()
    {
        return await ploegDAO.GetAll();
    }

}
