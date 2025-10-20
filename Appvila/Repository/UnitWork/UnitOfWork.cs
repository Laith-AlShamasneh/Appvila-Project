using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.UnitWork;

public class UnitOfWork : IUnitOfWork
{
    #region SystemManagement Repositories
    public IMasterAboutUs<MasterAboutUs> MasterAboutUs { get; }
    public IMasterAchievement<MasterAchievement> MasterAchievement { get; }
    public IMasterCallAction<MasterCallAction> MasterCallAction { get; }
    public IMasterCompany<MasterCompany> MasterCompany { get; }
    public IMasterFeature<MasterFeature> MasterFeature { get; }
    public IMasterFeatureItem<MasterFeatureItem> MasterFeatureItem { get; }
    public IMasterHeaderMenu<MasterHeaderMenu> MasterHeaderMenu { get; }
    public IMasterLegal<MasterLegal> MasterLegal { get; }
    public IMasterPricing<MasterPricing> MasterPricing { get; }
    public IMasterPricingItem<MasterPricingItem> MasterPricingItem { get; }
    public IMasterPricingItemFeature<MasterPricingItemFeature> MasterPricingItemFeature { get; }
    public IMasterSetting<MasterSetting> MasterSetting { get; }
    public IMasterSolution<MasterSolution> MasterSolution { get; }
    public IMasterSupport<MasterSupport> MasterSupport { get; }
    #endregion

    public UnitOfWork(
        IMasterAboutUs<MasterAboutUs> masterAboutUs,
        IMasterAchievement<MasterAchievement> masterAchievement,
        IMasterCallAction<MasterCallAction> masterCallAction,
        IMasterCompany<MasterCompany> masterCompany,
        IMasterFeature<MasterFeature> masterFeature,
        IMasterFeatureItem<MasterFeatureItem> masterFeatureItem,
        IMasterHeaderMenu<MasterHeaderMenu> masterHeaderMenu,
        IMasterLegal<MasterLegal> masterLegal,
        IMasterPricing<MasterPricing> masterPricing,
        IMasterPricingItem<MasterPricingItem> masterPricingItem,
        IMasterPricingItemFeature<MasterPricingItemFeature> masterPricingItemFeature,
        IMasterSetting<MasterSetting> masterSetting,
        IMasterSolution<MasterSolution> masterSolution,
        IMasterSupport<MasterSupport> masterSupport)
    {
        #region SystemManagement Repositories
        MasterAboutUs = masterAboutUs;
        MasterAchievement = masterAchievement;
        MasterCallAction = masterCallAction;
        MasterCompany = masterCompany;
        MasterFeature = masterFeature;
        MasterFeatureItem = masterFeatureItem;
        MasterHeaderMenu = masterHeaderMenu;
        MasterLegal = masterLegal;
        MasterPricing = masterPricing;
        MasterPricingItem = masterPricingItem;
        MasterPricingItemFeature = masterPricingItemFeature;
        MasterSetting = masterSetting;
        MasterSolution = masterSolution;
        MasterSupport = masterSupport;
        #endregion
    }
}
