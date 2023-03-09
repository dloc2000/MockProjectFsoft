using Fsoft.Web.Locdx7.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.DL
{
    public interface IOrderDetailDL
    {

        public Task<IQueryable> BuyProductOrderDetail(OrderDetail orderDetail);
        #region CRUD
        public Task<List<OrderDetail>> GetOrderDetails();

        public Task<OrderDetail> GetOrderDetailById(int id);

        public Task InsertOrderDetail(OrderDetail orderDetail);

        public Task UpdateOrderDetail(int id, OrderDetail orderDetailUpdate);

        public Task DeleteOrderDetail(int id);
        #endregion
    }
}
