using Buyer.Application.Contracts.Persistence;
using Buyer.Infrastructure.Persistence;
using Buyer.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Buyer.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddScoped<IBuyerContext, BuyerContext>();
            services.AddScoped<IBidRepository, BidRepository>();

            return services;
        }
    }
}
