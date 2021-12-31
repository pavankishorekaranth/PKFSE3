using Seller.Mediator.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Repositaries
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(string productId);
        Task CreateProduct(Product product);
        Task DeleteProduct(string productId);
    }
}
