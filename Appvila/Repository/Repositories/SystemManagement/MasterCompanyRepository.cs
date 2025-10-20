using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterCompanyRepository(IGlobalQueryAction<MasterCompany> globalQueryAction,
    IGlobalQueryListAction<MasterCompany> globalQueryListAction) : IMasterCompany<MasterCompany>
{
    private readonly IGlobalQueryAction<MasterCompany> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterCompany> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterCompany";

    public async Task<MasterCompany> GetById(MasterCompany entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterCompanyId", entity.MasterCompanyId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterCompany>> GetList(MasterCompany entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
