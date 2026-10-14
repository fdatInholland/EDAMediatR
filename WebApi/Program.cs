using ApplicationServices.Commands.Handlers;
using ApplicationServices.Queries.Handlers;
using DataLayer.Extensions;
using DomainLayer.Events.Handler;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(OrderByStartDateHandler).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(ApplyDiscountCommandHandler).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(OrderPlacedDiscountEventHandler).Assembly);
});

builder.Services.AddControllers();

var writeConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionWrite");
var readConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionRead");

builder.Services.AddDbDriver(writeConnectionString, readConnectionString);

foreach (var service in builder.Services)
{
    Console.WriteLine($"Type: {service.ServiceType.Name,-30} | " +
                      $"Lifetime: {service.Lifetime,-10} | " +
                      $"Impl: {service.ImplementationType?.Name ?? "Factory/Instance"}");

}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


