namespace Models.Common.Base;

/// <summary>
/// Represents the base pagination parameters for a paged request.
/// </summary>
public class BasePagination
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortColumn { get; set; }
    public string? SortColumnDirection { get; set; }
    public string? SearchValue { get; set; }
}

/// <summary>
/// Represents the base response for a paged query, including pagination metadata.
/// </summary>
public class BasePaginationResponse
{
    [JsonPropertyOrder(0)]
    public long TotalRecords { get; set; }
    [JsonPropertyOrder(1)]
    public int PageNumber { get; set; }
    [JsonPropertyOrder(2)]
    public int PageSize { get; set; }
}
