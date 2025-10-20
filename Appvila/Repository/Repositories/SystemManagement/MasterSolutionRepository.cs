using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterSolutionRepository(IGlobalQueryAction<MasterSolution> globalQueryAction,
    IGlobalQueryListAction<MasterSolution> globalQueryListAction) : IMasterSolution<MasterSolution>
{
    private readonly IGlobalQueryAction<MasterSolution> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterSolution> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterSolution";

    public async Task<MasterSolution> GetById(MasterSolution entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterSolutionId", entity.MasterSolutionId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterSolution>> GetList(MasterSolution entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
