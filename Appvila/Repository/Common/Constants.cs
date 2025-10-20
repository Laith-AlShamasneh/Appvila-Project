namespace Repository.Common;

/// <summary>
/// A class that holds application-level constants, currently for the database connection string.
/// </summary>
internal class Constants
{
    private static string _connectionString = string.Empty;

    internal static string ConnectionString
    {
        get => _connectionString;
        set => _connectionString = value ?? throw new ArgumentNullException(nameof(value), "ConnectionString cannot be null.");
    }
}

/// <summary>
/// Enum representing different database actions
/// </summary>
internal enum ActionName
{
    Select,
    SelectBy,
    SelectAll,
    Add,
    Update,
    Delete,
    AddGetList,
    UpdateGetList,
    DeleteGetList,
    ActiveGetList,
}

/// <summary>
/// Represents the sorting direction for pagination.
/// </summary>
public enum SortDirection
{
    ASC,
    DESC
}

/// <summary>
/// Represents pagination parameters for paged queries.
/// </summary>
public class PaginationParams
{
    /// <summary>
    /// The requested page number.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// The number of records per page.
    /// </summary>
    public int PageSize { get; set; }
}

/// <summary>
/// Utility class for naming stored procedures
/// </summary>
internal static class ProcedureNaming
{
    private const string ProcedurePrefix = "usp";

    /// <summary>
    /// Returns the formatted stored procedure name for tech admin operations.
    /// </summary>
    internal static string GetProcedureName(string tableName, string actionName)
        => $"{ProcedurePrefix}_{tableName}_{actionName}";
}
