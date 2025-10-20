using System.ComponentModel;

namespace Domain.Common;

/// <summary>
/// Represents supported languages.
/// </summary>
public enum Lang
{
    En = 1,
    Ar = 2
}

/// <summary>
/// Represents standard HTTP response statuses with descriptions.
/// </summary>
public enum HttpResponseStatus
{
    [Description("OK")]
    OK = 1,
    [Description("Created")]
    Created = 2,
    [Description("Accepted")]
    Accepted = 3,
    [Description("Found")]
    Found = 4,
    [Description("Bad Request")]
    BadRequest = 5,
    [Description("Unauthorized")]
    Unauthorized = 6,
    [Description("Forbidden")]
    Forbidden = 7,
    [Description("Data Not Found")]
    DataNotFound = 8,
    [Description("Method Not Allowed")]
    MethodNotAllowed = 9,
    [Description("Internal Server Error")]
    InternalServerError = 10,
    [Description("Request Timeout")]
    RequestTimeout = 11,
}

/// <summary>
/// Represents different types of system messages.
/// </summary>
public enum MessageType
{
    RetrieveSuccessfully,
    RetrieveFailed,
    SaveSuccessfully,
    SaveFailed,
    ActiveSuccessfully,
    ActiveFailed,
    DeleteSuccessfully,
    DeleteFailed,
    SystemProblem,
    NoDataFound
}
