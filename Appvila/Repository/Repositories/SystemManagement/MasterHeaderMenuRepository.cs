using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterHeaderMenuRepository(IGlobalQueryAction<MasterHeaderMenu> globalQueryAction,
    IGlobalQueryListAction<MasterHeaderMenu> globalQueryListAction) : IMasterHeaderMenu<MasterHeaderMenu>
{
    private readonly IGlobalQueryAction<MasterHeaderMenu> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterHeaderMenu> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterHeaderMenu";

    public async Task<MasterHeaderMenu> GetById(MasterHeaderMenu entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterHeaderMenuId", entity.MasterHeaderMenuId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterHeaderMenu>> GetList(MasterHeaderMenu entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
