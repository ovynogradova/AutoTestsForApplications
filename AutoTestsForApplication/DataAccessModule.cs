using apitest.Interfaces.DapperInterface;
using apitest.DapperRepository;
using Microsoft.Extensions.DependencyInjection;

namespace apitest;

public static class DataAccessModule
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUserRepository>(p => new UserRepository(connectionString));
        services.AddScoped<IAddressRepository>(p => new AddressRepository(connectionString));
        return services;
    }
}