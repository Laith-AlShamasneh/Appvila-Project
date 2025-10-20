namespace Domain.Entities.SystemManagement;

public class MasterPricingItemFeature
{
    public int MasterPricingItemFeatureId { get; set; }
    public int MasterPricingItemId { get; set; }
    public string Title { get; set; } = string.Empty;
}
