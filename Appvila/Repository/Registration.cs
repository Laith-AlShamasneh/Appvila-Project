using Domain.Entities.SystemManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interface.SystemManagement;
using Repository.Repositories;
using Repository.Repositories.SystemManagement;
using Repository.UnitWork;

namespace Repository;

public static class Registration
{
    /// <summary>
    /// Adds repository and infrastructure services to the dependency injection container for the application.
    /// </summary>
    public static void AddRepositoryInfraStructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region SystemManagement Repositories
        services.AddScoped<IMasterAboutUs<MasterAboutUs>, MasterAboutUsRepository>();
        services.AddScoped<IMasterAchievement<MasterAchievement>, MasterAchievementRepository>();
        services.AddScoped<IMasterCallAction<MasterCallAction>, MasterCallActionRepository>();
        services.AddScoped<IMasterCompany<MasterCompany>, MasterCompanyRepository>();
        services.AddScoped<IMasterFeature<MasterFeature>, MasterFeatureRepository>();
        services.AddScoped<IMasterFeatureItem<MasterFeatureItem>, MasterFeatureItemRepository>();
        services.AddScoped<IMasterHeaderMenu<MasterHeaderMenu>, MasterHeaderMenuRepository>();
        services.AddScoped<IMasterLegal<MasterLegal>, MasterLegalRepository>();
        services.AddScoped<IMasterPricing<MasterPricing>, MasterPricingRepository>();
        services.AddScoped<IMasterPricingItem<MasterPricingItem>, MasterPricingItemRepository>();
        services.AddScoped<IMasterPricingItemFeature<MasterPricingItemFeature>, MasterPricingItemFeatureRepository>();
        services.AddScoped<IMasterSetting<MasterSetting>, MasterSettingRepository>();
        services.AddScoped<IMasterSolution<MasterSolution>, MasterSolutionRepository>();
        services.AddScoped<IMasterSupport<MasterSupport>, MasterSupportRepository>();
        #endregion

        #region Generic Injection
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(IGlobalNonQueryAction<>), typeof(GlobalNonQueryAction<>));
        services.AddScoped(typeof(IGlobalQueryAction<>), typeof(GlobalQueryAction<>));
        services.AddScoped(typeof(IGlobalQueryListAction<>), typeof(GlobalQueryListAction<>));
        services.AddScoped(typeof(IGlobalScalarQueryAction<>), typeof(GlobalScalarQueryAction<>));
        services.AddScoped(typeof(IGlobalQueryListPaginationAction<>), typeof(GlobalQueryListPaginationAction<>));
        services.AddScoped(typeof(IGlobalQueryWithSuccessOutputAction<>), typeof(GlobalQueryWithSuccessOutputAction<>));
        services.AddScoped(typeof(IGlobalQueryMultipleDynamicAction), typeof(GlobalQueryMultipleDynamicAction));
        services.AddScoped(typeof(IGlobalQueryMultipleWithPaginationAction), typeof(GlobalQueryMultipleWithPaginationAction));
        #endregion

        Constants.ConnectionString = configuration.GetConnectionString("SqlConnection") ?? string.Empty;
    }
}
