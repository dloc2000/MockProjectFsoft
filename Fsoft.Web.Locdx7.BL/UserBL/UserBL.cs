using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.Common.Paging;
using Fsoft.Web.Locdx7.DL;
using System.Security.Cryptography;

namespace Fsoft.Web.Locdx7.BL
{
    public class UserBL : IUserBL
    {

        private IUserDL _UserDL;
        public UserBL(IUserDL userDL)
        {
            _UserDL = userDL;
        }

        // Validate
        #region Authentication
        public Task<bool> VerifyEmail(string token)
        {
            return _UserDL.VerifyEmail(token);
        }
        public async Task<string> LoginUser(User userRequest)
        {
            return await _UserDL.LoginUser(userRequest);
        }
        #endregion

        #region Paging
        public async Task<List<User>> GetUsersPaging(PagingFilter filter)
        {
            return await _UserDL.GetUsersPaging(filter);
        } 
        #endregion

        #region CRUD
        public async Task DeleteUser(int id)
        {
            await _UserDL.DeleteUser(id);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _UserDL.GetUserById(id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _UserDL.GetUsers(); 
        }


        public async Task InsertUser(User user)
        {
            // Validate
            user.VerifyContact = false;
            user.VerifyEmail = false;
            user.VerifityToken = CreateRandomToken();

            await  _UserDL.InsertUser(user);
        }

        public async Task UpdateUser(int id, User user)
        {
            await _UserDL.UpdateUser(id, user); 
        }
        #endregion

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

    }
}
