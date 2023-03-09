using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.BL
{
    public interface IUserBL
    {

        public Task<bool> VerifyEmail(string token);

        public Task<string> LoginUser(User user);

        public Task<List<User>> GetUsersPaging(PagingFilter filter);

        #region CRUD
        public Task<List<User>> GetUsers();

        public Task<User> GetUserById(int id);

        public Task InsertUser(User user);

        public Task UpdateUser(int id, User user);

        public Task DeleteUser(int id);
        #endregion
    }
}
