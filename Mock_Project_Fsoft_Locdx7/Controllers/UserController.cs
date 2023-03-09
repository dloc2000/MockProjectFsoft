using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.BL;
using Fsoft.Web.Locdx7.Common.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fsoft.Web.Locdx7.Common.Dto;

namespace Fsoft.Web.Locdx7.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserBL _userBL;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBL userBL, ILogger<UserController> logger)
        {
            _userBL = userBL;
            _logger = logger;   
        }

        /// <summary>
        /// API Verified Email
        /// </summary>
        /// <param name="token">token của user khi tạo</param>
        /// <returns></returns>
        /// Created by : Locdx7 (07/03/2023)
        #region AuthenticationEmail
        [HttpPost]
        [Route("verify")]
        public async Task<IActionResult> VerifiedEmail(string token)
        {
            try
            {
                if(await _userBL.VerifyEmail(token))
                {
                    return Ok("Verify Success");
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Verify failed");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("login/user")]
        public async Task<IActionResult> LoginUser([FromBody] UserRequestDTO userRequest)
        {
            try
            {
                _logger.LogInformation("Start logging");
                
                var tokenSuccess = await _userBL.LoginUser(new User { Username = userRequest.Email, Password = userRequest.Password });

                return Ok(new { message = "Login success", token = tokenSuccess });
            }
            catch (Exception)
            {
                throw;
                
            }
        }
        #endregion

        #region Paging
        [HttpGet]
        [Route("paging")]
        public async Task<IActionResult> GetUsersPaging([FromQuery] PagingFilter filter)
        {
            try
            {
                var users = await _userBL.GetUsersPaging(filter);

                return Ok(new { users, filter });
            }
            catch (Exception)
            {

                throw;
            }
        } 
        #endregion

        #region CRUD
        [HttpGet]
        public async Task<List<User>> GetAllUser()
        {
            try
            {
                return await _userBL.GetUsers();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserById([FromRoute] int id)
        {
            try
            {
                return await _userBL.GetUserById(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("a")]
        public async Task InsertUser(User user)
        {
            try
            {
                await _userBL.InsertUser(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task UpdateUser([FromRoute] int id, [FromBody] User user)
        {
            try
            {
                await _userBL.UpdateUser(id, user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteUser([FromRoute] int id)
        {
            try
            {
                await _userBL.DeleteUser(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
