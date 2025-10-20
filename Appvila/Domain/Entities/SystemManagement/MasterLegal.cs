using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterLegal : BaseEntity
{
    public int MasterLegalId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string LinkUrl { get; set; } = string.Empty;
}
