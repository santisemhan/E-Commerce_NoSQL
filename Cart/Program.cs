using Cart.Core.Contexts;
using Cart.Core.Helpers.Middleware;
using Cart.Core.Repositories;
using Cart.Core.Repositories.Contexts;
using Cart.Core.Repositories.Contexts.Interfaces;
using Cart.Core.Repositories.Interfaces;
using Cart.Core.Services;
using Cart.Core.Services.Interfaces;
using Microsoft.AspNetCore.Localization;
using StackExchange.Redis;
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

// Databases Connections
services.AddTransient<IConnection<IDatabase>, RedisDataContext>();
services.AddTransient<IConnection<Cassandra.ISession>, CassandraDataContext>();

// Services
services.AddTransient<IUserCartService, UserCartService>();

// Repositories
services.AddTransient<IUserCartRepository, UserCartRepository>();

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
