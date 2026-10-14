using DataLayer.Repo.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DataLayer.Extensions
{
    public static class OrderRepositoryExtensions
    {
        public static IServiceCollection AddAllOrderRepositories
            (this IServiceCollection services, Assembly assembly)
        {
            services.Scan(scan => scan
    .FromAssembliesOf(typeof(IReadRepository<>))
    .AddClasses(classes => classes.AssignableTo(typeof(IReadRepository<>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
    .AddClasses(classes => classes.AssignableTo(typeof(IWriteRepository<>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
);
            return services;
        }
    }
}
