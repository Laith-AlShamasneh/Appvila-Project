using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterFeature : BaseEntity
{
    public int MasterFeatureId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Brief { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
