using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterFeatureItem : BaseEntity
{
    public int MasterFeatureItemId { get; set; }
    public int MasterFeatureId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? IconClass { get; set; }
}
