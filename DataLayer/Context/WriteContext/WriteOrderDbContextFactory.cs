using DataLayer.Context.ReadContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Context.WriteContext
{
    public class WriteOrderDbContextFactory : IDesignTimeDbContextFactory<WriteOrderDbContext>
    {
        public WriteOrderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WriteOrderDbContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var writeConnectionString = configuration.GetConnectionString("DefaultConnectionWrite");

            optionsBuilder.UseSqlServer(writeConnectionString);

            return new WriteOrderDbContext(optionsBuilder.Options, null!);
        }
    }
}
