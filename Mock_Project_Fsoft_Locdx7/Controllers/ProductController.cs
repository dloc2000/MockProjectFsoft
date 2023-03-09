using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.BL;
using Fsoft.Web.Locdx7.Common.Paging;
using Microsoft.AspNetCore.Mvc;

namespace Fsoft.Web.Locdx7.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductBL _productBL;
        public ProductController(IProductBL ProductBL)
        {
            _productBL = ProductBL;
        }

        [HttpGet]
        [Route("paging")]
        public async Task<IActionResult> GetProductsPaging([FromQuery] PagingFilter filter)
        {
            try
            {
                var products = await _productBL.GetProductsPaging(filter);

                return Ok(new { products, filter });
            }
            catch (Exception)
            {

                throw;
            }
        }
        #region CRUD
        [HttpGet]
        public async Task<List<Product>> GetAllProduct()
        {
            try
            {
                return await _productBL.GetProducts();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProductById([FromRoute] int id)
        {
            try
            {
                return await _productBL.GetProductById(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("")]
        public async Task InsertProduct(Product product)
        {
            try
            {
                await _productBL.InsertProduct(product);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task UpdateProduct([FromRoute] int id, [FromBody] Product product)
        {
            try
            {
                await _productBL.UpdateProduct(id, product);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteProduct([FromRoute] int id)
        {
            try
            {
                await _productBL.DeleteProduct(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
