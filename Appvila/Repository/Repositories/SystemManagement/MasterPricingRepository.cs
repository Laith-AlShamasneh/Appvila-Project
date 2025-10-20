using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterPricingRepository(IGlobalQueryAction<MasterPricing> globalQueryAction,
    IGlobalQueryListAction<MasterPricing> globalQueryListAction) : IMasterPricing<MasterPricing>
{
    private readonly IGlobalQueryAction<MasterPricing> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterPricing> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterPricing";

    public async Task<MasterPricing> GetById(MasterPricing entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterPricingId", entity.MasterPricingId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterPricing>> GetList(MasterPricing entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
