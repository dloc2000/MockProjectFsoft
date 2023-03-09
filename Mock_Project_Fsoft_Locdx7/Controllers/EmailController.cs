using Fsoft.Web.Locdx7.Common.SendEmail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fsoft.Web.Locdx7.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private  readonly ISendEmailService _sendEmailService;

        public EmailController(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }

        [HttpPost]
        [Route("sendmail")]
        public async Task<IActionResult> SendEmail([FromBody] MailContent mailContent)
        {
            try
            {
                await _sendEmailService.SendMail(mailContent);

                return StatusCode(StatusCodes.Status200OK, "gửi mail thành công");
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

    }
}
