using Fsoft.Web.Locdx7.Common.Dto;
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
        public Task<ServiceResponse> GetUsers();

        public Task<ServiceResponse> GetUserById(int id);

        public Task<ServiceResponse> InsertUser(User user);

        public Task<ServiceResponse> UpdateUser(int id, User user);

        public Task<ServiceResponse> DeleteUser(int id);
        #endregion
    }
}
