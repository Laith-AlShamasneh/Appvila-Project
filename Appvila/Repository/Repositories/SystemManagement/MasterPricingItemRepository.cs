using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterPricingItemRepository(IGlobalQueryAction<MasterPricingItem> globalQueryAction,
    IGlobalQueryListAction<MasterPricingItem> globalQueryListAction) : IMasterPricingItem<MasterPricingItem>
{
    private readonly IGlobalQueryAction<MasterPricingItem> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterPricingItem> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterPricingItem";

    public async Task<MasterPricingItem> GetById(MasterPricingItem entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterPricingItemId", entity.MasterPricingItemId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterPricingItem>> GetByPricing(MasterPricingItem entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterPricingId", entity.MasterPricingId);

        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Pricing", parameters);
    }

    public async Task<IList<MasterPricingItem>> GetList(MasterPricingItem entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
