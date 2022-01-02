using Seller.Mediator.Library.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Repositaries
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(string productId);
        Task<Product> CreateProduct(Product product);
        Task DeleteProduct(string productId);
    }
}
