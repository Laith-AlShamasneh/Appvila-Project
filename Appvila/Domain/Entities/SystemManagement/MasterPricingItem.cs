using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterPricingItem : BaseEntity
{
    public int MasterPricingItemId { get; set; }
    public int MasterPricingId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PricePerMonth { get; set; }
    public int PricePerYear { get; set; }
    public string ButtonText { get; set; } = string.Empty;
    public string? LabelText { get; set; }
}
