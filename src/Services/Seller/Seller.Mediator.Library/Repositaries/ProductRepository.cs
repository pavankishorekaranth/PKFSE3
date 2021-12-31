using MongoDB.Driver;
using Seller.Mediator.Library.DataAccess;
using Seller.Mediator.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Repositaries
{
    public class ProductRepository
    {
        private readonly ISellerContext _context;

        public ProductRepository(ISellerContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductById(string productId)
        {
            return await _context.Products.Find(p => p.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task DeleteProduct(string productId)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.ProductId, productId);
            await _context.Products.DeleteOneAsync(filter);
        }
    }
}
