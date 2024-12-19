using HotelManagementSystem.Interfaces.Services;
using HotelManagementSystem.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HotelManagementSystem.Services.Utils
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("POSTGRESQLCONNSTR_AzureDb");
            services.AddDbContext<HotelDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<AuthSeeder>();
            services.AddScoped<HotelScope>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddTransient<IJwtTokenService, JwtTokenService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}