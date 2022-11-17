using Commerce.Core.Helpers.Middleware;
using Commerce.Core.Repositories;
using Commerce.Core.Repositories.Contexts;
using Commerce.Core.Repositories.Contexts.Interfaces;
using Commerce.Core.Repositories.Interfaces;
using Commerce.Core.Services;
using Commerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Localization;
using MongoDB.Driver;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var services = builder.Services;

services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddHttpContextAccessor();

services.Configure<RequestLocalizationOptions>(opts => {
    var supportedCultures = new List<CultureInfo> { new CultureInfo("es") };

    opts.DefaultRequestCulture = new RequestCulture("es");
    // Formatting numbers, dates, etc.
    opts.SupportedCultures = supportedCultures;
    // UI strings that we have localized.
    opts.SupportedUICultures = supportedCultures;
});

services.AddTransient<IConnection<IMongoDatabase>, MongoDataContext>();
services.AddTransient<IConnection<Cassandra.ISession>, CassandraDataContext>();

//Repositorios
services.AddTransient<ICatalogRepository, CatalogRepository>();
services.AddTransient<IOrderRepository, OrderRepository>();
services.AddTransient<IPaymentRepository, PaymentRepository>();
services.AddTransient<IProductRepository, ProductRepository>();
services.AddTransient<IUserRepository, UserRepository>();

//Servicios
services.AddTransient<ICatalogService, CatalogService>();
services.AddTransient<IOrderService, OrderService>();
services.AddTransient<IPaymentService, PaymentService>();
services.AddTransient<IProductService, ProductService>();
services.AddTransient<IUserService, UserService>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.RoutePrefix = "api/docs";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cart Service BD II");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
