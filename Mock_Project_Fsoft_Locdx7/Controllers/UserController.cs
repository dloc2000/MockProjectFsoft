using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.BL;
using Fsoft.Web.Locdx7.Common.Paging;
using Microsoft.AspNetCore.Mvc;
using Fsoft.Web.Locdx7.Common.Dto;
using Fsoft.Web.Locdx7.Common.Error;
using Fsoft.Web.Locdx7.Common.Enums;
using Fsoft.Web.Locdx7.Common.Resources;
using Microsoft.AspNetCore.Authorization;

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
                _logger.LogInformation("Start logging VerifiedEmail");
                var isCheckVerify = await _userBL.VerifyEmail(token);
                if (isCheckVerify)
                {
                    return Ok("Verify Success");
                }
                _logger.LogInformation($"Status verified : {isCheckVerify} ");

                return StatusCode(StatusCodes.Status400BadRequest, "Verify failed");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    FsoftErrorCode.Exception,
                    FsoftResource.DevMsg_Exception,
                    FsoftResource.UserMsg_Exception,
                    FsoftResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier)
                );
            }
        }

        [HttpPost]
        [Route("login/user")]
        public async Task<IActionResult> LoginUser([FromBody] UserRequestDTO userRequest)
        {
            try
            {
                _logger.LogInformation("Start logging LoginUser");
                
                var tokenSuccess = await _userBL.LoginUser(new User { Username = userRequest.Email, Password = userRequest.Password });

                return StatusCode(StatusCodes.Status200OK ,new { message = "Login success", token = tokenSuccess });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    FsoftErrorCode.Exception,
                    FsoftResource.DevMsg_Exception,
                    FsoftResource.UserMsg_Exception,
                    FsoftResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier)
                );
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

                return StatusCode(StatusCodes.Status200OK,new { users, filter });
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region CRUD
        /// <summary>
        /// API Get All Users
        /// </summary>
        /// <returns>Lấy tất cả các User</returns>
        /// Created by : Locdx7 (08/03/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                _logger.LogInformation("Start logging GetAllUser");

                var response =  await _userBL.GetUsers();
                if(response.Success)
                {

                _logger.LogInformation($"Data GetAllUser have {response.Data}");

                return StatusCode(StatusCodes.Status200OK, response);
                }

                _logger.LogInformation($"Data GetAllUser Failed :  {response.Data}");

                return StatusCode(StatusCodes.Status400BadRequest, response);


            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    FsoftErrorCode.Exception,
                    FsoftResource.DevMsg_Exception,
                    FsoftResource.UserMsg_Exception,
                    FsoftResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier)
                );
            }
        }
        /// <summary>
        /// API GetUserById
        /// </summary>
        /// <returns>Lấy User theo ID</returns>
        /// Created by : Locdx7 (08/03/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation("Start logging GetUserById");

                var response =  await _userBL.GetUserById(id);

                if (response.Success)
                {

                    _logger.LogInformation($"Data GetUserById have {response.Data}");
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                _logger.LogInformation($"Data GetUserById Failed :  {response.Data}");
                return StatusCode(StatusCodes.Status400BadRequest, response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    FsoftErrorCode.Exception,
                    FsoftResource.DevMsg_Exception,
                    FsoftResource.UserMsg_Exception,
                    FsoftResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier)
                );
            }
        }

        /// <summary>
        /// API InsertUser
        /// </summary>
        /// <returns>Thêm mới User</returns>
        /// Created by : Locdx7 (08/03/2023)
        [HttpPost]
        public async Task<IActionResult> InsertUser(User user)
        {
            try
            {
                _logger.LogInformation("Start logging InsertUser");

                var response = await _userBL.InsertUser(user);
                if (response.Success)
                {

                    _logger.LogInformation($"Data InsertUser have {response.Data}");
                    return StatusCode(StatusCodes.Status201Created, response);
                }

                _logger.LogInformation($"Data GetUserById Failed :  {response.Data}");
                return StatusCode(StatusCodes.Status400BadRequest, response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    FsoftErrorCode.Exception,
                    FsoftResource.DevMsg_Exception,
                    FsoftResource.UserMsg_Exception,
                    FsoftResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier)
                );
            }
        }

        /// <summary>
        /// API UpdateUser
        /// </summary>
        /// <returns>Sửa một User</returns>
        /// Created by : Locdx7 (08/03/2023)
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] User user)
        {
            try
            {
                _logger.LogInformation("Start logging UpdateUser");

                var response = await _userBL.UpdateUser(id, user);

                if (response.Success)
                {

                    _logger.LogInformation($"Data UpdateUser have {response.Data}");
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                _logger.LogInformation($"Data UpdateUser Failed :  {response.Data}");
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    FsoftErrorCode.Exception,
                    FsoftResource.DevMsg_Exception,
                    FsoftResource.UserMsg_Exception,
                    FsoftResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier)
                );
            }
        }

        /// <summary>
        /// API DeleteUser
        /// </summary>
        /// <returns>Xóa một User</returns>
        /// Created by : Locdx7 (08/03/2023)
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation("Start logging DeleteUser");

                var response = await _userBL.DeleteUser(id);

                if (response.Success)
                {

                    _logger.LogInformation($"Data DeleteUser have {response.Data}");
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                _logger.LogInformation($"Data DeleteUser Failed :  {response.Data}");
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    FsoftErrorCode.Exception,
                    FsoftResource.DevMsg_Exception,
                    FsoftResource.UserMsg_Exception,
                    FsoftResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier)
                );
            }
        }
        #endregion
    }

    /// <summary>
    /// Chức năng phân quyền
    /// </summary>
    /// <returns>Phân quyền xem là 1 User hay Admin</returns>
    /// Created by : Locdx7 (08/03/2023)
    public class CustomUserAuthorizeAttribute : AuthorizeAttribute
    {
        ////Entities context = new Entities(); // my entity  
        //private readonly Mock_Project_FSoft_locdx7Context _context;

        //private readonly string[] allowedroles;
        //public CustomUserAuthorizeAttribute(params string[] roles, Mock_Project_FSoft_locdx7Context context)
        //{
        //    this.allowedroles = roles;
        //    this._context = context;
        //}
        //protected override bool AuthorizeCore(HttpContext httpContext)
        //{
        //    bool authorize = false;
        //    foreach (var role in allowedroles)
        //    {
        //        var user = _context.Users.Where(m => m.UserId == GetUser.CurrentUser/* getting user form current context */ && m.Role == role &&
        //        m.VerifyEmail == true); // checking active users with allowed roles.  
        //        if (user.Count() > 0)
        //        {
        //            authorize = true; /* return true if Entity has current user(active) with specific role */
        //        }
        //    }
        //    return authorize;
        //}
        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    filterContext.Result = new HttpUnauthorizedResult();
        //}
    }
}
