using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.UnitWork;

/// <summary>
/// Defines the contract for a unit of work, which groups related database operations
/// and ensures that they are committed or rolled back as a single transaction.
/// </summary>
public interface IUnitOfWork
{
    #region SystemManagement Repositories
    IMasterAboutUs<MasterAboutUs> MasterAboutUs { get; }
    IMasterAchievement<MasterAchievement> MasterAchievement { get; }
    IMasterCallAction<MasterCallAction> MasterCallAction { get; }
    IMasterCompany<MasterCompany> MasterCompany { get; }
    IMasterFeature<MasterFeature> MasterFeature { get; }
    IMasterFeatureItem<MasterFeatureItem> MasterFeatureItem { get; }
    IMasterHeaderMenu<MasterHeaderMenu> MasterHeaderMenu { get; }
    IMasterLegal<MasterLegal> MasterLegal { get; }
    IMasterPricing<MasterPricing> MasterPricing { get; }
    IMasterPricingItem<MasterPricingItem> MasterPricingItem { get; }
    IMasterPricingItemFeature<MasterPricingItemFeature> MasterPricingItemFeature { get; }
    IMasterSetting<MasterSetting> MasterSetting { get; }
    IMasterSolution<MasterSolution> MasterSolution { get; }
    IMasterSupport<MasterSupport> MasterSupport { get; }
    #endregion
}
