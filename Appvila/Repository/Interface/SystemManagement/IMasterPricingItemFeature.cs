namespace Repository.Interface.SystemManagement;

public interface IMasterPricingItemFeature<MasterPricingItemFeature> : IGetListAction<MasterPricingItemFeature>, IGetByIdAction<MasterPricingItemFeature>
{
    Task<IList<MasterPricingItemFeature>> GetByPricingItem(MasterPricingItemFeature entity);
}
