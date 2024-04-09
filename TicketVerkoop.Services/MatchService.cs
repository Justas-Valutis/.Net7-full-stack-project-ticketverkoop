using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Services;

public class MatchService : IMatchService<Match>
{
    private IMatchDAO<Match> matchDAO;

    public MatchService(IMatchDAO<Match> _matchDAO)
    {
        matchDAO = _matchDAO;
    }
    public async Task<IEnumerable<Match>?> GetAll()
    {
        return await matchDAO.GetAll();
    }

    public async Task<IEnumerable<Match>?> GetMatchByStadiumId(int Id)
    {
        return await matchDAO.GetMatchByStadiumId(Id);
    }

    public async Task<IEnumerable<Match>?> GetMatchByPloegId(int Id)
    {
        return await matchDAO.GetMatchByPloegId(Id);
    }

    public async Task<IEnumerable<Match>?> GetMatchByPloegIdAndStadiumId(int PlegId, int StadiumId)
    {
        return await matchDAO.GetMatchByPloegIdAndStadiumId(PlegId, StadiumId);
    }
    public Task Add(Match entity)
    {
        throw new NotImplementedException();
    }

    public Task<Match?> FindById(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Match>?> GetAllWithHistory()
    {
        return await matchDAO.GetAllWithHistory();

    }
}
