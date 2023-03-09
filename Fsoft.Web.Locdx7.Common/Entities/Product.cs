using System;
using System.Collections.Generic;

namespace Fsoft.Web.Locdx7.Common.Entities
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public double? DiscountPercent { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public int? StockQuantity { get; set; }
        public int? Rating { get; set; }
        public string? PathImg { get; set; }
        public string? Description { get; set; }
        public int? Deleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
