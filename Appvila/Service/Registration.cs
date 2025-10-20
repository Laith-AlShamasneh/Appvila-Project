using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.CommonCall;
using Service.Interface.SystemManagement;
using Service.Services.SystemManagement;
using Repository;
using Service.Constants;
using Service.Helpers;

namespace Service;

public static class Registration
{
    /// <summary>
    /// Adds infrastructure services to the dependency injection container for the application.
    /// </summary>
    public static void AddServicesInfraStructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region SchoolManagement Services
        services.AddScoped(typeof(IHomeService<>), typeof(HomeService<>));
        #endregion

        #region Generic Injection
        services.AddScoped(typeof(IServiceBroker<>), typeof(ServiceBroker<>));
        services.AddSingleton<LocalizationHelper>();
        #endregion Generic Injection

        services.AddRepositoryInfraStructure(configuration);

        ServiceConstants.BaseUrl = configuration["Setting:BaseUrl"] ?? string.Empty;
        ServiceConstants.AssetsPath = configuration["Setting:AssetsPath"] ?? string.Empty;
    }
}