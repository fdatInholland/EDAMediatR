using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Context.ReadContext
{
    public class ReadOrderDbContextFactory : IDesignTimeDbContextFactory<ReadOrderDbContext>
    {
        public ReadOrderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReadOrderDbContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var readConnectionString = configuration.GetConnectionString("DefaultConnectionRead");

            optionsBuilder.UseSqlServer(readConnectionString);

            return new ReadOrderDbContext(optionsBuilder.Options, null!);
        }
    }
}
