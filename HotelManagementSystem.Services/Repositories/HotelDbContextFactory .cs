using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HotelManagementSystem.Services.Repositories
{
    public class HotelDbContextFactory(IConfiguration configuration) : IDesignTimeDbContextFactory<HotelDbContext>
    {
        public HotelDbContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_AzureDb");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new System.ArgumentException($"Connection string not found {Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_AzureDb")}");
            }

            var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new HotelDbContext(optionsBuilder.Options);
        }
    }
}
