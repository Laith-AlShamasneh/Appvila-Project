using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterHeaderMenu : BaseEntity
{
    public int MasterHeaderMenuId { get; set; }
    public int? ParentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
    public string? LinkUrl { get; set; }
    public string? Tooltip { get; set; }
}
