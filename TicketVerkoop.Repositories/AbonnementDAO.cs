using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories;

public class AbonnementDAO
{
    private readonly SoccerDbContext _dbContext;

    public AbonnementDAO()
    {
        _dbContext = new SoccerDbContext();
    }

}
