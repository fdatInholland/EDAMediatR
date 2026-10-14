using DomainLayer.Models.Customers;
using DomainLayer.Models.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context.ReadContext
{
    public class ReadOrderDbContext : DbContext
    {
        //# Add migration for ReadOrderDbContext
        //dotnet ef migrations add InitialRead --context ReadOrderDbContext

        //# Update the App database
        //dotnet ef database update --context ReadOrderDbContext
        private readonly IMediator _mediator;
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Customer> Customers => Set<Customer>();

        public ReadOrderDbContext(DbContextOptions<ReadOrderDbContext> options, IMediator? mediator = null)
        : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder
        builder)
        {
        }

    }
}
