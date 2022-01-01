﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using MediatR;
using FluentValidation;
using Seller.Mediator.Library.DataAccess;
using Seller.Mediator.Library.Repositaries;

namespace Seller.Mediator.Library
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<ISellerContext,SellerContext>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
    
}
