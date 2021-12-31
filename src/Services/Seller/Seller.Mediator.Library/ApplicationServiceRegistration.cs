using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using MediatR;
using FluentValidation;

namespace Seller.Mediator.Library
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
    
}
