using Fsoft.Web.Locdx7.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.DL
{
    public interface IOrderDL
    {

        #region CRUD
        public Task<List<Order>> GetOrders();

        public Task<Order> GetOrderById(int id);

        public Task InsertOrder(Order order);

        public Task UpdateOrder(int id, Order orderUpdate);

        public Task DeleteOrder(int id);
        #endregion
    }
}
