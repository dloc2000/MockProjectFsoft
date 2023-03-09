using Fsoft.Web.Locdx7.BL;
using Fsoft.Web.Locdx7.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fsoft.Web.Locdx7.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {

        private readonly IOderDetailBL _oderDetailBL;

        public OrderDetailController(IOderDetailBL oderDetailBL)
        {
            _oderDetailBL = oderDetailBL;
        }

        #region BuyProductOrderDetail

        [HttpPost]
        [Route("buyproduct")]
        public async Task<IActionResult> BuyProductOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region CRUD
        [HttpGet]
        public async Task<List<OrderDetail>> GetAllOrder()
        {
            try
            {
                return await _oderDetailBL.GetOrderDetails();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<OrderDetail> GetOrderDetailById([FromRoute] int id)
        {
            try
            {
                return await _oderDetailBL.GetOrderDetailById(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("")]
        public async Task InsertOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                await _oderDetailBL.InsertOrderDetail(orderDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task UpdateOrderDetail([FromRoute] int id, [FromBody] OrderDetail orderDetail)
        {
            try
            {
                await _oderDetailBL.UpdateOrderDetail(id, orderDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteOrderDetail([FromRoute] int id)
        {
            try
            {
                await _oderDetailBL.DeleteOrderDetail(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
