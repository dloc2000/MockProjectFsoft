using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.Common.Enums
{
    public class Verification
    {
      
        public enum VerificationEmail
        {
            NotVerified = 0,
            Verified = 1
        }

        public enum VerificationContact
        {
            NotVerified = 0,
            Verified = 1
        }

        public enum Role
        {
            Admin = 0,
            User = 1
        }
    }
}
