using Infrastructure.Data;
using Infrastructure.IConfiguration;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDbInitializer, DbInitializer>();

            return services;
        }

        public static IServiceCollection AddGenericRepository(this IServiceCollection service)
        {
            //Infrastructure
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return service;
        }

    }
}
