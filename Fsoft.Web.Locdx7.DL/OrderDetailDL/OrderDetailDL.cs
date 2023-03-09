using Fsoft.Web.Locdx7.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.DL
{
    public class OrderDetailDL : IOrderDetailDL
    {

        private readonly Mock_Project_FSoft_locdx7Context _context;
        public OrderDetailDL(Mock_Project_FSoft_locdx7Context ctx)
        {
            _context = ctx;
        }

        #region BuyProductOrderDetail
        public async Task<IQueryable> BuyProductOrderDetail(OrderDetail orderDetail)
        {
            var orderDetailAfter =   _context.OrderDetails
                .Join(
                   _context.Products,
                   o => o.ProductId,
                   p => p.ProductId,
                   (o, p) => new { Product = p, OrderDetail = o }
                )
                .Select(op => new
                {
                    TotalAmount = op.Product.StockQuantity * Convert.ToInt32(op.Product.Price)
                });
            
               return orderDetailAfter;
        }
        #endregion
        #region CRUD
        public async Task DeleteOrderDetail(int id)
        {
            OrderDetail orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<OrderDetail> GetOrderDetailById(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        }

        public async Task<List<OrderDetail>> GetOrderDetails()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task InsertOrderDetail(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderDetail(int id, OrderDetail orderDetailUpdate)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail != null)
            {
                // Đánh dấu rằng sản phẩm đã bị thay đổi
                // Update 1 object ,ko cần update từng thuộc tính
                _context.Entry(orderDetail).CurrentValues.SetValues(orderDetailUpdate);
                await _context.SaveChangesAsync();
            }
        } 
        #endregion
    }
}
