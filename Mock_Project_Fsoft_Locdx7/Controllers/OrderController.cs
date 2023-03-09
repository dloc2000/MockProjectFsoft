using Fsoft.Web.Locdx7.BL;
using Fsoft.Web.Locdx7.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fsoft.Web.Locdx7.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOderBL _oderBL;

        public OrderController(IOderBL oderBL)
        {
            _oderBL = oderBL;
        }

        #region CRUD
        [HttpGet]
        public async Task<List<Order>> GetAllOrder()
        {
            try
            {
                return await _oderBL.GetOrders();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<Order> GetOrderById([FromRoute] int id)
        {
            try
            {
                return await _oderBL.GetOrderById(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("")]
        public async Task InsertOrder(Order order)
        {
            try
            {
                await _oderBL.InsertOrder(order);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task UpdateOrder([FromRoute] int id, [FromBody] Order order)
        {
            try
            {
                await _oderBL.UpdateOrder(id, order);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteOrder([FromRoute] int id)
        {
            try
            {
                await _oderBL.DeleteOrder(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
