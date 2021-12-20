using Seller.API.Entity;
using Seller.API.Exceptions;
using Seller.API.Repositaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.API.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task CreateProduct(Product product)
        {
            if (product.BidEndDate < DateTime.Now)
                throw new BidEndDateException("Bid End Date should be greater than today's date");

            await productRepository.CreateProduct(product);
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            var product = await productRepository.GetProductById(productId);
            if (product == null)
                throw new ProductNotExist($"Product with Id {productId} doesn't exist");

            if (product.BidEndDate < DateTime.Now)
                throw new DeleteProductAfterBidEndDate($"Product cannot be deleted after Bind End Date");

            return await productRepository.DeleteProduct(productId);
        }

        public async Task<Product> GetProductById(string productId)
        {
            var product = await productRepository.GetProductById(productId);
            if (product == null)
                throw new ProductNotExist($"Product with Id {productId} doesn't exist");

            return product;
        }
    }
}
