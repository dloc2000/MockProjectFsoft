using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.DL
{
    public interface IProductDL
    {
        public Task<List<Product>> GetProductsPaging(PagingFilter filter);

        #region CRUD
        public Task<List<Product>> GetProducts();

        public Task<Product> GetProductById(int id);

        public Task InsertProduct(Product product);

        public Task UpdateProduct(int id , Product productUpdate);

        public Task DeleteProduct(int id);
        #endregion
    }
}
