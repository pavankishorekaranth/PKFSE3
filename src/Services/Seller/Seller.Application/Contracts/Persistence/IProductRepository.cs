using Seller.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seller.Application.Contracts.Persistence
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(string productId);
        Task<Product> CreateProduct(Product product);
        Task DeleteProduct(string productId);
    }
}
