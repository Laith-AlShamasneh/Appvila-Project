using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterFeatureRepository(IGlobalQueryAction<MasterFeature> globalQueryAction,
    IGlobalQueryListAction<MasterFeature> globalQueryListAction) : IMasterFeature<MasterFeature>
{
    private readonly IGlobalQueryAction<MasterFeature> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterFeature> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterFeature";

    public async Task<MasterFeature> GetById(MasterFeature entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterFeatureId", entity.MasterFeatureId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterFeature>> GetList(MasterFeature entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
