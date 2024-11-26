using Domain.DomainService;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class RegisterServices
{
    public static void RegisterDomainServices(this IServiceCollection  serviceCollection)
    {
        serviceCollection.AddScoped<IUserDomainService, UserDomainDomainService>();
    }
}