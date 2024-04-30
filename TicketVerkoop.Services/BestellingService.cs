using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Services;

public class BestellingService : IService<Bestelling>
{
    private IDAO<Bestelling> bestellingDAO;

    public BestellingService(IDAO<Bestelling> _bestellingDAO)
    {
        bestellingDAO = _bestellingDAO;
    }
    public async Task<int> AddandGetID(Bestelling entity)
    {
        return await bestellingDAO.AddandGetID(entity);
    }

    public Task Add(Bestelling entity)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Bestelling entity)
    {
        await bestellingDAO.Delete(entity);
    }

    public async Task<Bestelling?> FindById(int Id)
    {
        return await bestellingDAO.FindById(Id);
    }

    public async Task<Bestelling?> Get(int v)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Bestelling>?> GetAll()
    {
        return await bestellingDAO.GetAll();
    }
}
