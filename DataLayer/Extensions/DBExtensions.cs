using DataLayer.Context.ReadContext;
using DataLayer.Repo.UofW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer.Extensions
{
    public static class DBExtensions
    {
        public static IServiceCollection AddDbDriver(
           this IServiceCollection services,
           string writeconnectionString,
           string readconnectionString)
        {
            services.AddDbContext<IUnitOfWork, WriteOrderDbContext>(options =>
                options.UseSqlServer(writeconnectionString,
                    b => b.MigrationsAssembly("DataLayer")));

            //NB for reads use the NoTracking alternative!!!
            services.AddDbContext<ReadOrderDbContext>(options => options.UseSqlServer(readconnectionString
                ).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddAllOrderRepositories(typeof(DBExtensions).Assembly);

            return services;
        }
    }
}
