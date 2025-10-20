using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterPricing : BaseEntity 
{
    public int MasterPricingId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Brief { get; set; } = string.Empty;
    public string? Description { get; set; }
}
