using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterPricingItemFeatureRepository(IGlobalQueryAction<MasterPricingItemFeature> globalQueryAction,
    IGlobalQueryListAction<MasterPricingItemFeature> globalQueryListAction) : IMasterPricingItemFeature<MasterPricingItemFeature>
{
    private readonly IGlobalQueryAction<MasterPricingItemFeature> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterPricingItemFeature> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterPricingItemFeature";

    public async Task<MasterPricingItemFeature> GetById(MasterPricingItemFeature entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterPricingItemFeatureId", entity.MasterPricingItemFeatureId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterPricingItemFeature>> GetByPricingItem(MasterPricingItemFeature entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterPricingItemId", entity.MasterPricingItemId);

        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}PricingItem", parameters);
    }

    public async Task<IList<MasterPricingItemFeature>> GetList(MasterPricingItemFeature entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
