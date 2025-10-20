using Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Repository.UnitWork;

namespace Service.Helpers;

internal class GenericResponseHandler<TController>
{
    private readonly ILogger<TController> _logger;
    private readonly IUserContext _userContext;

    internal GenericResponseHandler(ILogger<TController> logger, IUserContext userContext)
    {
        _logger = logger;
        _userContext = userContext;
    }

    /// <summary>
    /// Handles internal server errors and returns a standardized error response.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with an internal server error status.</returns>
    internal StandardApiResponse HandleListInternalServerError(int innerLayoutOrientation, Exception ex)
    {
        _logger.LogError("An internal server error occurred: {ErrorMessage}", ex.Message);
        return ResponseHandler.HandleResultNullList(false, (int)HttpResponseStatus.InternalServerError, innerLayoutOrientation, MessageType.SystemProblem, _userContext);
    }

    /// <summary>
    /// Handles internal server errors and returns a standardized error response for an object result.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with an internal server error status.</returns>
    internal StandardApiResponse HandleObjectInternalServerError(int innerLayoutOrientation, Exception ex)
    {
        _logger.LogError("An internal server error occurred: {ErrorMessage}", ex.Message);
        return ResponseHandler.HandleResultNullObject(false, (int)HttpResponseStatus.InternalServerError, innerLayoutOrientation, MessageType.SystemProblem, _userContext);
    }

    /// <summary>
    /// Handles bad request errors and returns a standardized error response for a list result.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with a bad request status.</returns>
    internal StandardApiResponse HandleListBadRequest(int innerLayoutOrientation, MessageType messageType)
    {
        return ResponseHandler.HandleResultNullList(false, (int)HttpResponseStatus.BadRequest, innerLayoutOrientation, messageType, _userContext);
    }

    /// <summary>
    /// Handles bad request errors and returns a standardized error response for an object result.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with a bad request status.</returns>
    internal StandardApiResponse HandleObjectBadRequest(int innerLayoutOrientation, MessageType messageType)
    {
        return ResponseHandler.HandleResultNullObject(false, (int)HttpResponseStatus.BadRequest, innerLayoutOrientation, messageType, _userContext);
    }

    /// <summary>
    /// Handles a not found response for a list.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with a DataNotFound status.</returns>
    internal StandardApiResponse HandleListNotFound(int innerLayoutOrientation, MessageType messageType)
    {
        return ResponseHandler.HandleResultNullList(true, (int)HttpResponseStatus.DataNotFound, innerLayoutOrientation, messageType, _userContext);
    }

    /// <summary>
    /// Handles a not found response for an object.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with a DataNotFound status.</returns>
    internal StandardApiResponse HandleObjectNotFound(int innerLayoutOrientation, MessageType messageType)
    {
        return ResponseHandler.HandleResultNullObject(true, (int)HttpResponseStatus.DataNotFound, innerLayoutOrientation, messageType, _userContext);
    }

    /// <summary>
    /// Handles a successful response for a list.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with an OK status and list data.</returns>
    internal StandardApiResponse HandleListSuccess<TReturn>(int innerLayoutOrientation, IList<TReturn> entity, MessageType messageType)
    {
        return ResponseHandler.HandleResultList<TReturn>(true, (int)HttpResponseStatus.OK, innerLayoutOrientation, messageType, entity, _userContext);
    }

    /// <summary>
    /// Handles a successful response for an object.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with an OK status and object data.</returns>
    internal StandardApiResponse HandleObjectSuccess<TReturn>(int innerLayoutOrientation, TReturn entity, MessageType messageType)
    {
        return ResponseHandler.HandleResultObject<TReturn>(true, (int)HttpResponseStatus.OK, innerLayoutOrientation, messageType, entity, _userContext);
    }

    /// <summary>
    /// Handles a successful response with a null list.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with an OK status.</returns>
    internal StandardApiResponse HandleNullListSuccess(int innerLayoutOrientation, MessageType messageType)
    {
        return ResponseHandler.HandleResultNullList(true, (int)HttpResponseStatus.OK, innerLayoutOrientation, messageType, _userContext);
    }

    /// <summary>
    /// Handles a successful response with a null object.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with an OK status.</returns>
    internal StandardApiResponse HandleNullObjectSuccess(int innerLayoutOrientation, MessageType messageType)
    {
        return ResponseHandler.HandleResultNullObject(true, (int)HttpResponseStatus.OK, innerLayoutOrientation, messageType, _userContext);
    }
}

/// <summary>
/// Generic handler for response handling.
/// </summary>
internal class Handler<TController> : GenericResponseHandler<TController>
{
    internal Handler(ILogger<TController> logger, IUserContext userContext) : base(logger, userContext) { }
}

/// <summary>
/// Handler with UnitOfWork support.
/// </summary>
internal class HandlerWithUnitOfWork<TController> : GenericResponseHandler<TController>
{
    internal readonly IUnitOfWork _unitOfWork;
    internal readonly IUserContext _userContext;
    internal readonly StandardServiceResponse serviceResponse;

    internal HandlerWithUnitOfWork(ILogger<TController> logger, IUnitOfWork unitOfWork, IUserContext userContext) : base(logger, userContext)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        serviceResponse = new();
    }
}

/// <summary>
/// Handler with UnitOfWork, configuration, and hosting environment support.
/// </summary>
internal class HandlerWithUnitOfWorkAndHost<TController> : GenericResponseHandler<TController>
{
    internal readonly IUnitOfWork _unitOfWork;
    internal readonly IConfiguration _configuration;
    internal readonly IHostingEnvironment _hostEnvironment;
    internal readonly IUserContext _userContext;
    internal readonly StandardServiceResponse serviceResponse;

    internal HandlerWithUnitOfWorkAndHost(ILogger<TController> logger, IUnitOfWork unitOfWork, IConfiguration configuration, IHostingEnvironment hostEnvironment, IUserContext userContext)
        : base(logger, userContext)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
        serviceResponse = new();
        _userContext = userContext;
    }
}
