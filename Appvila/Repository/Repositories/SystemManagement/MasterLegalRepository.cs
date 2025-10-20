using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterLegalRepository(IGlobalQueryAction<MasterLegal> globalQueryAction,
    IGlobalQueryListAction<MasterLegal> globalQueryListAction) : IMasterLegal<MasterLegal>
{
    private readonly IGlobalQueryAction<MasterLegal> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterLegal> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterLegal";

    public async Task<MasterLegal> GetById(MasterLegal entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterLegalId", entity.MasterLegalId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterLegal>> GetList(MasterLegal entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
