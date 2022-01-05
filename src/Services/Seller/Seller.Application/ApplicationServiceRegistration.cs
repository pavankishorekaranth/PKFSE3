using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using MediatR;
using FluentValidation;
//using Seller.Application.DataAccess;
//using Seller.Application.Repositaries;
using Seller.Application.Behaviour;

namespace Seller.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddScoped<ISellerContext,SellerContext>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IBidRepository, BidRepository>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
    
}
