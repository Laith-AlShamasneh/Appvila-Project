using Domain.Common;

namespace Service.Helpers;

public static class ResponseHandler
{

    /// <summary>
    /// Handles the result of an operation that returns a single object. 
    /// This method creates a response with a status code, success flag, message, and the result object.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> object representing the outcome of the operation.</returns>
    internal static StandardApiResponse HandleResultObject<TReturn>(bool success, int code, int innerLayoutOrientation, MessageType messageType, TReturn result, IUserContext userContext)
    {
        return new()
        {
            Code = code,
            Success = success,
            Message = Methods.GetMessages(userContext.LangId, messageType),
            Data = result!
        };
    }

    /// <summary>
    /// Handles the result of an operation that returns a list of objects.
    /// This method creates a response with a status code, success flag, message, and a list of results.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> object representing the outcome of the operation.</returns>
    internal static StandardApiResponse HandleResultList<TReturn>(bool success, int code, int innerLayoutOrientation, MessageType messageType, IList<TReturn> result, IUserContext userContext)
    {
        return new()
        {
            Code = code,
            Success = success,
            Message = Methods.GetMessages(userContext.LangId, messageType),
            Data = result
        };
    }

    /// <summary>
    /// Handles the result of an operation that returns a null object.
    /// This method creates a response with a status code, success flag, message, and a null object as the data.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> object representing the outcome of the operation.</returns>
    internal static StandardApiResponse HandleResultNullObject(bool success, int code, int innerLayoutOrientation, MessageType messageType, IUserContext userContext)
    {
        return new()
        {
            Code = code,
            Success = success,
            Message = Methods.GetMessages(userContext.LangId, messageType),
            Data = new NullColumns()
        };
    }

    /// <summary>
    /// Handles the result of an operation that returns a null list.
    /// This method creates a response with a status code, success flag, message, and a null list as the data.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> object representing the outcome of the operation.</returns>
    internal static StandardApiResponse HandleResultNullList(bool success, int code, int innerLayoutOrientation, MessageType messageType, IUserContext userContext)
    {
        return new()
        {
            Code = code,
            Success = success,
            Message = Methods.GetMessages(userContext.LangId, messageType),
            Data = new List<NullColumns>()
        };
    }
}
