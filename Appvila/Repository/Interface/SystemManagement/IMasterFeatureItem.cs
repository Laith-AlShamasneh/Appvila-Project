namespace Repository.Interface.SystemManagement;

public interface IMasterFeatureItem<MasterFeatureItem> : IGetListAction<MasterFeatureItem>, IGetByIdAction<MasterFeatureItem>
{
    Task<IList<MasterFeatureItem>> GetByFeature(MasterFeatureItem entity);
}
