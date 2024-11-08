﻿using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories;

public class PloegDAO : IDAO<Ploeg>
{
    private readonly SoccerDbContext _dbContext;

    public PloegDAO()
    {
        _dbContext = new SoccerDbContext();
    }
    public Task Add(Ploeg entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddandGetID(Ploeg entity)
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

    public async Task<IEnumerable<Ploeg>?> GetAll()
    {
        try
        {
            return await _dbContext.Ploegs
                .Include(s => s.ThuisStadium)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("ERROR IN DAO" + ex.Message);
        }
    }

    public Task<IEnumerable<Ploeg>?> GetAllByUserId(string UserId)
    {
        throw new NotImplementedException();
    }
}
