using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class RegisterServices
    {
        public static void RegisterInfrastructureServices(this IServiceCollection serviceCollection,IConfiguration configuration)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IUnit, Unit>();
            
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));
        }
    }
}
