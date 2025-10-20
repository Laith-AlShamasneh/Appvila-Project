using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterAboutUs : BaseEntity
{
    public int MasterAboutUsId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool HasAppStoreButton { get; set; }
    public string? AppStoreButtonText { get; set; }
    public string? AppStoreButtonLinkUrl { get; set; }
    public string? AppStoreButtonIconClass { get; set; }
    public bool HasGooglePlayButton { get; set; }
    public string? GooglePlayButtonText { get; set; }
    public string? GooglePlayButtonLinkUrl { get; set; }
    public string? GooglePlayButtonIconClass { get; set; }
    public string? ImageUrl { get; set; }
}
