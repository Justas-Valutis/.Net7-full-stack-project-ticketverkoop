using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVerkoop.Domains.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;

namespace TicketVerkoop.Repositories
{
    public class UserDAO : IDAO<AspNetUser>
    {
        private readonly SoccerDbContext _dbContext;
        public UserDAO()
        {
            _dbContext = new SoccerDbContext();
        }
        public Task Add(AspNetUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddandGetID(AspNetUser entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(AspNetUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<AspNetUser?> FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AspNetUser>?> GetAll()
        {
            try
            {
                return await _dbContext.AspNetUsers
                    .Include(b => b.Bestellings)
                    //.ThenInclude(bestellings => bestellings.Abonnements)
                    //.ThenInclude(abonnements => abonnements.Ploeg)
                    //.ThenInclude(ploeg => ploeg.ThuisStadium)
                    //.Include(b => b.Bestellings)
                    //.ThenInclude(bestellings => bestellings.Tickets)
                    //.ThenInclude(tickets => tickets.Match)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("ERROR IN DAO" + ex.Message);
            }
        }

        public Task<IEnumerable<AspNetUser>?> GetAllByUserId(string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
