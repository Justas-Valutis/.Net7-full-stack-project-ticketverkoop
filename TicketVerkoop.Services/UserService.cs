using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Services
{
    public class UserService : IService<AspNetUser>
    {
        private IDAO<AspNetUser> _userDao;
        public UserService(IDAO<AspNetUser> _userDao)
        {
            this._userDao = _userDao;
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

        public Task<AspNetUser> FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<AspNetUser?> Get(int v)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AspNetUser>?> GetAll()
        {
            return await _userDao.GetAll();
        }

        public Task<IEnumerable<AspNetUser>?> GetAllByUserId(string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
