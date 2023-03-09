using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.BL
{
    public class OrderBL : IOderBL
    {
        private readonly IOrderDL _orderDL;

        public OrderBL(IOrderDL orderDL)
        {
            _orderDL = orderDL;
        }


        #region CRUD
        public async Task DeleteOrder(int id)
        {
            await _orderDL.DeleteOrder(id);
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _orderDL.GetOrderById(id);
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _orderDL.GetOrders();
        }

        public async Task InsertOrder(Order order)
        {
            await _orderDL.InsertOrder(order);
        }

        public async Task UpdateOrder(int id, Order orderUpdate)
        {
            await _orderDL.UpdateOrder(id, orderUpdate);    
        } 
        #endregion
    }
}
