using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.BL;
using Fsoft.Web.Locdx7.Common.Paging;
using Microsoft.AspNetCore.Mvc;
using Fsoft.Web.Locdx7.Common.Enums;
using Fsoft.Web.Locdx7.Common.Error;
using Fsoft.Web.Locdx7.Common.Resources;

namespace Fsoft.Web.Locdx7.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductBL _productBL;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductBL ProductBL, ILogger<ProductController> logger)
        {
            _productBL = ProductBL;
            _logger = logger;
        }

        [HttpGet]
        [Route("paging")]
        public async Task<IActionResult> GetProductsPaging([FromQuery] PagingFilter filter)
        {
            try
            {
                _logger.LogInformation("Start logging GetProductsPaging");

                var products = await _productBL.GetProductsPaging(filter);

                _logger.LogInformation($"have {products.Count} product");

                return StatusCode(StatusCodes.Status200OK ,new { products, filter });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    FsoftErrorCode.Exception,
                    FsoftResource.DevMsg_Exception,
                    FsoftResource.UserMsg_Exception,
                    FsoftResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier)
                );
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
