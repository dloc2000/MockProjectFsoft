using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.Common.SendEmail
{
    public interface ISendEmailService
    {

        Task SendMail(MailContent mailContent);

        Task SendMailAsync(string email, string subject, string htmlMessage);
    }
}
