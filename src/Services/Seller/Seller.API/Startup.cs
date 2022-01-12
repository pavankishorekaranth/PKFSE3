using EventBus.Message.Common;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Seller.API.EventBusConsumer;
using Seller.Application;
using Seller.Infrastructure;
using System.Reflection;

namespace Seller.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddInfrastructureService();

            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            bool.TryParse(Configuration["BaseServiceSettings:UserabbitMq"], out var useRabbitMq);
            
            //RabbitMq/Service Bus configurations using Masstransit
            services.AddMassTransit(config =>
            {
                config.AddConsumer<CreateBidConsumer>();
                config.AddConsumer<UpdateBidConsumer>();

                if (useRabbitMq)
                {
                    config.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host(Configuration["RabbitMQSettings:HostAddress"]);

                        cfg.ReceiveEndpoint(EventBusConstants.CreateBidQueue, c =>
                        {
                            c.ConfigureConsumer<CreateBidConsumer>(ctx);
                        });
                        cfg.ReceiveEndpoint(EventBusConstants.UpdateBidQueue, c =>
                        {
                            c.ConfigureConsumer<UpdateBidConsumer>(ctx);
                        });
                    });
                }
                else
                {
                    config.UsingAzureServiceBus((ctx, cfg) =>
                    {
                        cfg.Host(Configuration["AzureServiceBusQueueBusSettings:ConnectionString"]);

                        cfg.ReceiveEndpoint(EventBusConstants.CreateBidQueue, c =>
                        {
                            c.ConfigureConsumer<CreateBidConsumer>(ctx);
                        });
                        cfg.ReceiveEndpoint(EventBusConstants.UpdateBidQueue, c =>
                        {
                            c.ConfigureConsumer<UpdateBidConsumer>(ctx);
                        });
                    });
                }
            });
            services.AddMassTransitHostedService();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Seller.API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Seller.API v1"));
            //}
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
