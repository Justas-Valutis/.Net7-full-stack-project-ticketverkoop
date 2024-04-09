using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Services;

public class SectionService : IGetAllByService<Section>
{
    private IGetAllByDAO<Section> sectionDAO;

    public SectionService(IGetAllByDAO<Section> _sectionDAO)
    {
        sectionDAO = _sectionDAO;
    }
    public Task<IEnumerable<Section>?> GetAllBy(int Id)
    {
        return sectionDAO.GetAllBy(Id);
    }
}
