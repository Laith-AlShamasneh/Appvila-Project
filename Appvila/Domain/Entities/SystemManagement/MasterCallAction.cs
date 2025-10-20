using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterCallAction : BaseEntity
{
    public int MasterCallActionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool HasButton { get; set; }
    public string? ButtonText { get; set; }
}
