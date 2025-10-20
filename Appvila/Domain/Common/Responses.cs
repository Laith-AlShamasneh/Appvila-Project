namespace Domain.Common;

/// <summary>
/// Represents a standard API response format.
/// </summary>
public class StandardApiResponse
{
    public int Code { get; set; }
    public bool Success { get; set; }
    public object Message { get; set; } = new();
    public int InnerLayoutOrientation { get; set; }
    public object Data { get; set; } = new();
}

/// <summary>
/// Represents a standard response format for service-layer operations.
/// </summary>
public class StandardServiceResponse
{
    public bool IsValide { get; set; }
    public int InnerLayoutOrientation { get; set; }
    public object Data { get; set; } = new();
}

/// <summary>
/// Represents a placeholder or container for columns that may have null values.
/// This class may be used in scenarios where the columns in a data structure can be null.
/// </summary>
public class NullColumns
{

}
