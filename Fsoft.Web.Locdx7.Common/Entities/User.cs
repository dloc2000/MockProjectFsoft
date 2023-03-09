using System;
using System.Collections.Generic;

namespace Fsoft.Web.Locdx7.Common.Entities
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? Role { get; set; }
        public string? Avatar { get; set; }
        public bool? Status { get; set; }
        public string? Contact { get; set; }
        public string? PathImg { get; set; }
        public bool? VerifyEmail { get; set; }
        public bool? VerifyContact { get; set; }
        public int? Deleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? VerifityToken { get; set; }
        public DateTime? VerifiyAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
