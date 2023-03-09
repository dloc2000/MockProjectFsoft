using System;
using System.Collections.Generic;

namespace Fsoft.Web.Locdx7.Common.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public decimal? Amount { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public DateTime? Date { get; set; }
        public bool? Paided { get; set; }
        public string? Status { get; set; }
        public int? Deleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
