using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.Common.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.DL
{
    public class ProductDL : IProductDL
    {
        private readonly Mock_Project_FSoft_locdx7Context _context;
        public ProductDL(Mock_Project_FSoft_locdx7Context ctx)
        {
            _context = ctx;
        }

        public async Task<List<Product>> GetProductsPaging(PagingFilter filter)
        {
            var validFilter = new PagingFilter(filter.PageNumber, filter.PageSize);

            var productPaging = await _context.Products
                                 .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                 .Take(validFilter.PageSize)
                                 .ToListAsync();
            return productPaging;
        }
        public async Task DeleteProduct(int id)
        {
            Product product = await _context.Products.FindAsync(id);

            if(product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
             return await _context.Products.FindAsync(id);
        }
        

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task InsertProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(int id, Product productUpdate)
        {
            var product  = await _context.Products.FindAsync(id);

            if(product != null)
            {
                // Đánh dấu rằng sản phẩm đã bị thay đổi
                // Update 1 object ,ko cần update từng thuộc tính
                _context.Entry(product).CurrentValues.SetValues(productUpdate);
                await  _context.SaveChangesAsync();
            }
        }
    }
}
