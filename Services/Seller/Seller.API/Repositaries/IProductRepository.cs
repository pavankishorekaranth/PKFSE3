using Seller.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.API.Repositaries
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(string productId);
        Task CreateProduct(Product product);
        Task DeleteProduct(string productId);
    }
}
