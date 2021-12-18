using Seller.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.API.Repositaries
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);
        Task<bool> DeleteProduct(string productId);
    }
}
