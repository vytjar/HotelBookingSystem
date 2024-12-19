using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HotelManagementSystem.Services.Repositories
{
    public class HotelDbContextFactory(IConfiguration configuration) : IDesignTimeDbContextFactory<HotelDbContext>
    {
        public HotelDbContext CreateDbContext(string[] args)
        {
            // Set up configuration to read from appsettings.json
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            // Retrieve the connection string
            var connectionString = configuration.GetConnectionString("POSTGRESQLCONNSTR_AzureDb");

            // Set up DbContextOptions with the Npgsql provider for PostgreSQL
            var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new HotelDbContext(optionsBuilder.Options);
        }
    }
}
