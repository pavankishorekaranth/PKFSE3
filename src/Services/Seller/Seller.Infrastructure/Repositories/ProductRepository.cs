using MongoDB.Driver;
using Seller.Domain.Entities;
using Seller.Application.Contracts.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seller.Infrastructure.Repositaries
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISellerContext _context;

        public ProductRepository(ISellerContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProductById(string productId)
        {
            return await _context.Products.Find(p => p.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task DeleteProduct(string productId)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            await _context.Products.DeleteOneAsync(filter);
        }
    }
}
