using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.Common.SendEmail
{
    public class MailContent
    {

        public string To { get; set; } // Địa chỉ gửi đến

        public string Subject { get; set; } // Tiêu đề mail

        public string Body { get; set; } // Nội dung
    }
}
