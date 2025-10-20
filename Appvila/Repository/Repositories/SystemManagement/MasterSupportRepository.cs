using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterSupportRepository(IGlobalQueryAction<MasterSupport> globalQueryAction,
    IGlobalQueryListAction<MasterSupport> globalQueryListAction) : IMasterSupport<MasterSupport>
{
    private readonly IGlobalQueryAction<MasterSupport> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterSupport> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterSupport";

    public async Task<MasterSupport> GetById(MasterSupport entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterSupportId", entity.MasterSupportId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterSupport>> GetList(MasterSupport entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
