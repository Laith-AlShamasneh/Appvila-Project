using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterSupport : BaseEntity
{
    public int MasterSupportId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string LinkUrl { get; set; } = string.Empty;
}
