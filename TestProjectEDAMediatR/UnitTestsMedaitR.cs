using ApplicationServices.Commands;
using ApplicationServices.Commands.Handlers;
using DataLayer.Repo.Interfaces;
using DomainLayer.Models.Order;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TestProjectEDAMediatR
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MediatR_Should_Register_ApplyDiscountCommand()
        {
            var services = new ServiceCollection();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplyDiscountCommandHandler).Assembly);
            });

            var serviceProvider = services.BuildServiceProvider();

            var handler = serviceProvider.GetService<IRequestHandler<ApplyDiscountCommand, Guid>>();

            Assert.That(handler, Is.Not.Empty);
            Assert.That(handler, Has.Some.TypeOf<ApplyDiscountCommandHandler>());
        }

        [Test]
        public void Should_Resolve_IReadRepository_For_Order()
        {
            // 1. Arrange: Setup service collection and run your Scrutor scan setup
            var services = new ServiceCollection();

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IReadRepository<>))
                .AddClasses(classes => classes.AssignableTo(typeof(IReadRepository<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IWriteRepository<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            // 2. Act: Build the service provider and open a scope
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();

            // Resolve the specific interface instance
            var repository = scope.ServiceProvider.GetService<IReadRepository<Order>>();

            // 3. Assert: Verify resolution was successful
            Assert.That(repository, Is.Not.Null);
        }
    }
}
