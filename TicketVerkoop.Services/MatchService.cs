using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Services;

public class MatchService : IService<Match>
{
    private IDAO<Match> matchDAO;

    public MatchService(IDAO<Match> _matchDAO)
    {
        matchDAO = _matchDAO;
    }
    public Task AddA(Match entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Match entity)
    {
        throw new NotImplementedException();
    }

    public Task<Match?> FindById(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<Match?> Get(int v)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Match>?> GetAll()
    {
        return await matchDAO.GetAll();
    }

    public Task Update(Match entity)
    {
        throw new NotImplementedException();
    }
}
