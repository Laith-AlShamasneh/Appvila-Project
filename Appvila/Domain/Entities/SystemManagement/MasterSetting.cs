using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterSetting : BaseEntity
{
    public int MasterSettingId { get; set; }
    public string HeaderLogoUrl { get; set; } = string.Empty;
    public string FooterLogoUrl { get; set; } = string.Empty;
    public string? FooterDescription { get; set; }
    public string? DesignedBy { get; set; }
}
