using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.Common.Error
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }


        public ErrorDetail()
        {

        }
        public ErrorDetail(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;  
        }
    }
}
