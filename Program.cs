using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RavenShop.Application.Data.Context;
using RavenShop.Application.Data.Repositories;
using RavenShop.Application.Models.Persistence;
using RavenShop.Application.Models.Product;
using RavenShop.Application.Services.v1;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();

ConfigureApp(app);

app.Run();


void RegisterServices(IServiceCollection services)
{
    services.Configure<PersistenceSettings>(builder.Configuration.GetSection("Database"));
    services.AddSingleton<ProductService>();
    services.AddSingleton(typeof(IRavenDbRepository<>), typeof(RavenDbRepository<>));
    services.AddSingleton<IRavenDbContext, RavenDbContext>();
    services.AddSingleton(new MapperConfiguration(config =>
    {
        config.CreateMap<ProductModel, Product>();
        config.CreateMap<Product, ProductModel>();
    }).CreateMapper());
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Shop",
            Description = "Shop administration",
            Version = "v1"
        });
    });
}

void ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();

    #region Endpoints

    // This method is Post because it is necessary to send the id via Body. The reason for this is that the id in RavenDB has a slash,
    // example "products/78-A", if this id is sent via url, it will result in a 404 - Not Found.
    // If you can find a more elegant way to do this, I would be very happy to evaluate a Pull request.

    app.MapPost("v1/products/find_one", (ProductService service, SelectProduct selectProduct) =>
    {
        var product = service.FindOne(selectProduct);

        if (product is null)
            return Results.NotFound();

        return Results.Ok(product);

    }).WithName("FindOneProduct");

    app.MapGet("v1/products", (ProductService service, int pageSize, int pageNumber) =>
    {
        var products = service.FindAll(pageSize, pageNumber);

        if (!products.Any())
            return Results.NotFound();

        return Results.Ok(products);

    }).WithName("FindAllProducts");

    app.MapPost("v1/products", (ProductService service, ProductModel product) =>
    {
        var errorMessage = service.Create(product);

        if (string.IsNullOrEmpty(errorMessage))
            return Results.Ok();

        return Results.BadRequest(errorMessage);

    }).WithName("CreateProduct");

    app.MapPut("v1/products", (ProductService service, ProductModel product) =>
    {
        var errorMessage = service.Update(product);

        if (string.IsNullOrEmpty(errorMessage))
            return Results.Ok();

        return Results.BadRequest(errorMessage);

    }).WithName("UpdateProduct");

    app.MapDelete("v1/products", (ProductService service, string id) =>
    {
        var errorMessage = service.Delete(id);

        if (string.IsNullOrEmpty(errorMessage))
            return Results.Ok();

        return Results.BadRequest(errorMessage);

    }).WithName("DeleteProduct");
    #endregion
}