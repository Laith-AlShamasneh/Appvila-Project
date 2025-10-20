using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterAchievement : BaseEntity
{
    public int MasterAchievementId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? SatisfactionValue { get; set; }
    public int? HapyUserValue { get; set; }
    public int? DownloadValue { get; set; }
}
