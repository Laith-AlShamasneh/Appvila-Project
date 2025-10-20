using Domain.Entities.SystemManagement;
using Repository.Interface.SystemManagement;

namespace Repository.Repositories.SystemManagement;

internal class MasterAboutUsRepository(IGlobalQueryAction<MasterAboutUs> globalQueryAction,
    IGlobalQueryListAction<MasterAboutUs> globalQueryListAction) : IMasterAboutUs<MasterAboutUs>
{
    private readonly IGlobalQueryAction<MasterAboutUs> _globalQueryAction = globalQueryAction;
    private readonly IGlobalQueryListAction<MasterAboutUs> _globalQueryListAction = globalQueryListAction;
    private readonly string _tableName = "MasterAboutUs";

    public async Task<MasterAboutUs> GetById(MasterAboutUs entity)
    {
        DynamicParameters parameters = new();
        parameters.Add("@MasterAboutUsId", entity.MasterAboutUsId);

        return await _globalQueryAction.ExecuteQueryCommand($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectBy))}Id", parameters);
    }

    public async Task<IList<MasterAboutUs>> GetList(MasterAboutUs entity)
    {
        return await _globalQueryListAction.ExecuteQueryCommandList($"{ProcedureNaming.GetProcedureName(_tableName, nameof(ActionName.SelectAll))}", new DynamicParameters());
    }
}
