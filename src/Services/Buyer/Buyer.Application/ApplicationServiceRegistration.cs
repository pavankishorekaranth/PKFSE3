using Microsoft.Extensions.DependencyInjection;
using System;

namespace Buyer.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            return services;
        }
    }
}
