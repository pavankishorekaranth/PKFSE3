using Buyer.Application;
using Buyer.Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace Buyer.API
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
            services.AddInfrastructureService();
            services.AddApplicationService();

            services.AddControllers().AddNewtonsoftJson(opt => {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            bool.TryParse(Configuration["BaseServiceSettings:UserabbitMq"], out var useRabbitMq);
            services.AddMassTransit(config=> {
                if (useRabbitMq)
                {
                    config.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host("amqp://guest:guest@localhost:5672");
                    });
                }
                else
                {
                    config.UsingAzureServiceBus((ctx, cfg) =>
                    {
                        cfg.Host(Configuration["AzureServiceBusQueueBusSettings:ConnectionString"]);
                    });
                }
            });
            services.AddMassTransitHostedService();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Buyer.API", Version = "v1" });
            });

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Buyer.API v1"));
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
