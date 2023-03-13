using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.DL
{
    public interface IUserDL
    {

        public Task<bool> VerifyEmail(string token);

        public Task<string> LoginUser(User user);

        public Task<List<User>> GetUsersPaging(PagingFilter filter);

        #region CRUD
        public Task<List<User>> GetUsers();

        public Task<User> GetUserById(int id);

        public Task<int> InsertUser(User product);

        public Task<int> UpdateUser(int id, User product);

        public Task<int> DeleteUser(int id);
        #endregion
    }
}
