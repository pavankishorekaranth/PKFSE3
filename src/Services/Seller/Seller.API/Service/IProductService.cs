using Seller.API.Entity;
using System.Threading.Tasks;

namespace Seller.API.Service
{
    public interface IProductService
    {
        Task<Product> GetProductById(string productId);
        Task CreateProduct(Product product);
        Task<bool> DeleteProduct(string productId);
    }
}
