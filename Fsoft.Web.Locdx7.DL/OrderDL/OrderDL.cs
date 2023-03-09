using Fsoft.Web.Locdx7.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.DL
{
    public class OrderDL : IOrderDL
    {
        private readonly Mock_Project_FSoft_locdx7Context _context;
        public OrderDL(Mock_Project_FSoft_locdx7Context ctx)
        {
            _context = ctx;
        }

        #region CRUD
        public async Task DeleteOrder(int id)
        {
            Order order = await _context.Orders.FindAsync(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task InsertOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(int id, Order orderUpdate)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order != null)
            {
                // Đánh dấu rằng sản phẩm đã bị thay đổi
                // Update 1 object ,ko cần update từng thuộc tính
                _context.Entry(order).CurrentValues.SetValues(orderUpdate);
                await _context.SaveChangesAsync();
            }
        } 
        #endregion
    }
}
