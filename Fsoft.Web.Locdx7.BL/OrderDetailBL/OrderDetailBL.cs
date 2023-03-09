using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.BL
{
    public class OrderDetailBL : IOderDetailBL
    {
        private readonly IOrderDetailDL _orderDL;
        public OrderDetailBL(IOrderDetailDL orderDL)
        {
            _orderDL = orderDL;
        }

        #region CRUD
        public async Task DeleteOrderDetail(int id)
        {
            await _orderDL.DeleteOrderDetail(id);
        }

        public async Task<OrderDetail> GetOrderDetailById(int id)
        {
            return await _orderDL.GetOrderDetailById(id);
        }

        public async Task<List<OrderDetail>> GetOrderDetails()
        {
            return await _orderDL.GetOrderDetails();
        }

        public async Task InsertOrderDetail(OrderDetail orderDetail)
        {
            await _orderDL.InsertOrderDetail(orderDetail);
        }

        public async Task UpdateOrderDetail(int id, OrderDetail orderDetailUpdate)
        {
            await _orderDL.UpdateOrderDetail(id, orderDetailUpdate);
        } 
        #endregion
    }
}
