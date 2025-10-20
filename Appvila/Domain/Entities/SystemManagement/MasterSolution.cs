using Domain.Base;

namespace Domain.Entities.SystemManagement;

public class MasterSolution : BaseEntity
{
    public int MasterSolutionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string LinkUrl { get; set; } = string.Empty;
}
