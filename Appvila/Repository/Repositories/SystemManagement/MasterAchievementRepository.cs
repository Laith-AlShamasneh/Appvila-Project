using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterAchievementRepository(IGlobalQueryAction<MasterAchievement> globalQueryAction,
    IGlobalQueryListAction<MasterAchievement> globalQueryListAction) : IMasterAchievement<MasterAchievement>
{
    private readonly IGlobalQueryAction<MasterAchievement> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterAchievement> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterAchievement";

    public async Task<MasterAchievement> GetById(MasterAchievement entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterAchievementId", entity.MasterAchievementId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterAchievement>> GetList(MasterAchievement entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
