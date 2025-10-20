namespace Repository.Interface.SystemManagement;

public interface IMasterPricingItem<MasterPricingItem> : IGetListAction<MasterPricingItem>, IGetByIdAction<MasterPricingItem>
{
    Task<IList<MasterPricingItem>> GetByPricing(MasterPricingItem entity);
}
