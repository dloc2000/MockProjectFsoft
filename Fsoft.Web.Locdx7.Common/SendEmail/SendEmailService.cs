using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.Common.SendEmail
{
    public class SendEmailService : ISendEmailService
    {


        private readonly IConfiguration _configuration;

        private readonly ILogger<SendEmailService> _logger;

      
        // Có inject Logger để xuất log
        public SendEmailService(ILogger<SendEmailService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _logger.LogInformation("Create SendMailService");
            _configuration = configuration;
        }

        public async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            var _mailSettings = _configuration.GetSection("MailSettings");
        
            email.Sender = new MailboxAddress(_mailSettings["DisplayName"], _mailSettings["Mail"]);
            email.From.Add(new MailboxAddress(_mailSettings["DisplayName"], _mailSettings["Mail"]));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;

            // mail dùng văn bản html 
            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            // dùng StmpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(_mailSettings["Host"], 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings["Mail"], _mailSettings["Password"]);
                Console.WriteLine(_mailSettings["Mail"] + _mailSettings["Password"]);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {

                // Gửi mail thất bại, nội dung email sẽ lưu vào mailssave
                System.IO.Directory.CreateDirectory("mailssave");
                var emailSaveFile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailSaveFile);
                
                _logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailSaveFile);
                _logger.LogError(ex.Message);
            }

            smtp.Disconnect(true);

            _logger.LogInformation("Send mail to " + mailContent.To);
        }

        public async Task SendMailAsync(string email, string subject, string htmlMessage)
        {
            await SendMail(new MailContent()
            {
                To = email,
                Subject = subject,
                Body = htmlMessage
            });
        }
    }
}
