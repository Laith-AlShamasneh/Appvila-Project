using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterSettingRepository(IGlobalQueryAction<MasterSetting> globalQueryAction,
    IGlobalQueryListAction<MasterSetting> globalQueryListAction) : IMasterSetting<MasterSetting>
{
    private readonly IGlobalQueryAction<MasterSetting> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterSetting> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterSetting";

    public async Task<MasterSetting> GetById(MasterSetting entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterSettingId", entity.MasterSettingId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterSetting>> GetList(MasterSetting entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
