using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterCallActionRepository(IGlobalQueryAction<MasterCallAction> globalQueryAction,
    IGlobalQueryListAction<MasterCallAction> globalQueryListAction) : IMasterCallAction<MasterCallAction>
{
    private readonly IGlobalQueryAction<MasterCallAction> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterCallAction> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterCallAction";

    public async Task<MasterCallAction> GetById(MasterCallAction entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterCallActionId", entity.MasterCallActionId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterCallAction>> GetList(MasterCallAction entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
