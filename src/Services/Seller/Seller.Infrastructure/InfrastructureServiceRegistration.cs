using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Seller.Application.Contracts.Persistence;
using Seller.Infrastructure.Persistence;
using Seller.Infrastructure.Repositaries;

namespace Seller.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddScoped<ISellerContext, SellerContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBidRepository, BidRepository>();

            return services;
        }
    }
}
