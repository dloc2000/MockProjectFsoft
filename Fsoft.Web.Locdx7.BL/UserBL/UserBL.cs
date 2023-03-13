using Fsoft.Web.Locdx7.Common.Dto;
using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.Common.Enums;
using Fsoft.Web.Locdx7.Common.Error;
using Fsoft.Web.Locdx7.Common.Paging;
using Fsoft.Web.Locdx7.Common.Resources;
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
            var isVerify =  _UserDL.VerifyEmail(token);
            if(isVerify.Result)
            {
                throw new ApplicationException("Invalid token");
            }
            return isVerify;
        }
        public async Task<string> LoginUser(User userRequest)
        {
            var token =  await _UserDL.LoginUser(userRequest);
            if(token == null)
            {
                throw new ApplicationException("User invalid");
            }
            return token;
        }
        #endregion

        #region Paging
        public async Task<List<User>> GetUsersPaging(PagingFilter filter)
        {
            var lstUser =  await _UserDL.GetUsersPaging(filter);

            if(lstUser.Count < 0 )
            {
                throw new ApplicationException("User is empty");
            }
            return lstUser;
        } 
        #endregion

        #region CRUD
        public async Task<ServiceResponse> DeleteUser(int id)
        {
            var idDel = await _UserDL.DeleteUser(id);

            if(idDel != 0 )
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = idDel
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Data = new ErrorResult(
                        FsoftErrorCode.DeleteFailed,
                        FsoftResource.DevMsg_DeleteFailed,
                        FsoftResource.UserMsg_DeleteFailed,
                        FsoftResource.MoreInfo_DeleteFailed
                       )
            };
        }

        public async Task<ServiceResponse> GetUserById(int id)
        {
            var user =  await _UserDL.GetUserById(id);
            if(user != null)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = user
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Data = "Get User By Id Failed"
            };
        }

        public async Task<ServiceResponse> GetUsers()
        {
            var lstUser = await _UserDL.GetUsers(); 

            if(lstUser.Count > 0 )
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = lstUser
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Data = "Get Users Failed"
            };
        }


        public async Task<ServiceResponse> InsertUser(User user)
        {
            // Validate
            user.VerifyContact = false;
            user.VerifyEmail = false;
            user.VerifityToken = CreateRandomToken();

            var idInsert = await  _UserDL.InsertUser(user);

            if(idInsert > 0)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = idInsert
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Data = new ErrorResult(
                        FsoftErrorCode.InsertFailed,
                        FsoftResource.DevMsg_InsertFailed,
                        FsoftResource.UserMsg_InsertFailed,
                        FsoftResource.MoreInfo_InsertFailed
                       )
            };
        }

        public async Task<ServiceResponse> UpdateUser(int id, User user)
        {
           var idUpdate =  await _UserDL.UpdateUser(id, user); 

            if(idUpdate > 0 )
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = idUpdate
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Data = new ErrorResult(
                        FsoftErrorCode.UpdateFailed,
                        FsoftResource.DevMsg_UpdatedFailed,
                        FsoftResource.UserMsg_UpdatedFailed,
                        FsoftResource.MoreInfo_UpdatedFailed
                       )
            };
        }
        #endregion

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

    }
}
