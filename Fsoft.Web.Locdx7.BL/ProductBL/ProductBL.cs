using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.Common.Paging;
using Fsoft.Web.Locdx7.DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.BL
{
    public class ProductBL : IProductBL
    {

        private IProductDL _productDL;
        public ProductBL(IProductDL productDL) 
        {
            _productDL = productDL;
        }


        public async Task<List<Product>> GetProductsPaging(PagingFilter filter)
        {
            
            return await _productDL.GetProductsPaging(filter);
        }

        #region CRUD
        public Task DeleteProduct(int id)
        {
            return _productDL.DeleteProduct(id);
        }

        public Task<Product> GetProductById(int id)
        {
           return _productDL.GetProductById(id);
        }

        public Task<List<Product>> GetProducts()
        {
            return _productDL.GetProducts();
        }

        public Task InsertProduct(Product product)
        {
            return _productDL.InsertProduct(product);
        }

        public Task UpdateProduct(int id, Product productUpdate)
        {
            return _productDL.UpdateProduct(id, productUpdate);
        }
        #endregion
    }
}
