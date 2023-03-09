using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.Common.Dto
{
    public class UserRequestDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public UserRequestDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }   
    }
}
