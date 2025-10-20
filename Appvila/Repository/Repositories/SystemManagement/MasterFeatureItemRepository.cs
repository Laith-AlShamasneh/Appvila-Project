using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterFeatureItemRepository(IGlobalQueryAction<MasterFeatureItem> globalQueryAction,
    IGlobalQueryListAction<MasterFeatureItem> globalQueryListAction) : IMasterFeatureItem<MasterFeatureItem>
{
    private readonly IGlobalQueryAction<MasterFeatureItem> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterFeatureItem> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterFeatureItem";

    public async Task<IList<MasterFeatureItem>> GetByFeature(MasterFeatureItem entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterFeatureId", entity.MasterFeatureId);

        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Feature", parameters);
    }

    public async Task<MasterFeatureItem> GetById(MasterFeatureItem entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterFeatureItemId", entity.MasterFeatureItemId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterFeatureItem>> GetList(MasterFeatureItem entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
