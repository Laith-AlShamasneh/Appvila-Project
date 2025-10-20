using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterCompany : BaseEntity
{
    public int MasterCompanyId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string LinkUrl { get; set; } = string.Empty;
}
