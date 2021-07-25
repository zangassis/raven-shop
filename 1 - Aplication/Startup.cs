using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShopWebAPI.Aplication.Models;
using ShopWebAPI.Domain.Entities;
using ShopWebAPI.Infra.Context;
using ShopWebAPI.Infra.Persistence;
using ShopWebAPI.Infra.Repositories;

namespace ShopWebAPI.Aplication
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopWebAPI", Version = "v1" });
            });
            services.AddSingleton(typeof(IRepository<>), typeof(RavenDbRepository<>));
            services.AddSingleton<IRavenDbContext, RavenDbContext>();

            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<ProductModel, Product>();
            }).CreateMapper());

            services.Configure<PersistenceSettings>(Configuration.GetSection("Database"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopWebAPI v1"));
            }

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
