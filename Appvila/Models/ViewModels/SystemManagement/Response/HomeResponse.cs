namespace Models.ViewModels.SystemManagement.Response;

public class HeaderMenuResponseVM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
    public string? LinkUrl { get; set; }
    public string? Tooltip { get; set; }
}

public class AboutUsResponseVM
{
    public int Id { get; set; }
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

public class FeatureItemVM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? IconClass { get; set; }
}

public class FeatureResponseVM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Brief { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IEnumerable<FeatureItemVM> Items { get; set; } = [];
}

public class AchievementResponseVM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? SatisfactionValue { get; set; }
    public string? HapyUserValue { get; set; }
    public string? DownloadValue { get; set; }
}

public class PricingItemFeatureVM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}

public class PricingItemVM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PricePerMonth { get; set; }
    public int PricePerYear { get; set; }
    public string ButtonText { get; set; } = string.Empty;
    public string? LabelText { get; set; }
    public IEnumerable<PricingItemFeatureVM> Features { get; set; } = [];
}

public class PricingResponseVM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Brief { get; set; } = string.Empty;
    public string? Description { get; set; }
    public IEnumerable<PricingItemVM> Items { get; set; } = [];
}

public class CallActionResponseVM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool HasButton { get; set; }
    public string? ButtonText { get; set; }
}

public class LookupItemVM
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string LinkUrl { get; set; } = string.Empty;
}

public class FooterMenusResponseVM
{
    public IEnumerable<LookupItemVM> Solutions { get; set; } = [];
    public IEnumerable<LookupItemVM> Support { get; set; } = [];
    public IEnumerable<LookupItemVM> Company { get; set; } = [];
    public IEnumerable<LookupItemVM> Legal { get; set; } = [];
}

public class SettingResponseVM
{
    public int Id { get; set; }
    public string HeaderLogoUrl { get; set; } = string.Empty;
    public string FooterLogoUrl { get; set; } = string.Empty;
    public string? FooterDescription { get; set; }
    public string? DesignedBy { get; set; }
}