using Domain.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service.Helpers;

namespace Service.CommonCall;

internal class ServiceBroker<TController>(ILogger<TController> Logger, IConfiguration configuration, IUserContext userContext) : Handler<TController>(Logger, userContext), IServiceBroker<TController>
{
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// Handles a request for a list of entities, calling a method asynchronously, 
    /// and returns appropriate responses based on the method result and modal state.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with the result of the operation.</returns>
    public async Task<StandardApiResponse> ReturnListMethodsWithoutEntity<TReturn>(Func<Task<StandardServiceResponse>> method, bool modalState, MessageType successMsgType, MessageType notFoundMsgType, MessageType badRequestMsgType)
    {
        StandardApiResponse apiResponse = new();
        try
        {
            if (modalState)
            {
                StandardServiceResponse standardServiceResponse = await method.Invoke();

                var configValueString = _configuration["InnerLayoutOrientation"];
                int configValue = int.TryParse(configValueString, out var val) ? val : 0;

                apiResponse.InnerLayoutOrientation = standardServiceResponse.InnerLayoutOrientation > 0
                    ? standardServiceResponse.InnerLayoutOrientation
                    : configValue;

                if (standardServiceResponse.IsValide)
                {
                    apiResponse = HandleListSuccess<TReturn>(standardServiceResponse.InnerLayoutOrientation, (IList<TReturn>)standardServiceResponse.Data, successMsgType);
                }
                else
                {
                    apiResponse = HandleListNotFound(standardServiceResponse.InnerLayoutOrientation, notFoundMsgType);
                }
            }
            else
            {
                apiResponse = HandleListBadRequest(apiResponse.InnerLayoutOrientation, badRequestMsgType);
            }
        }
        catch (SqlException ex)
        {
            apiResponse = HandleListInternalServerError(apiResponse.InnerLayoutOrientation, ex);
        }
        catch (Exception ex)
        {
            apiResponse = HandleListInternalServerError(apiResponse.InnerLayoutOrientation, ex);
        }
        return apiResponse;
    }

    /// <summary>
    /// Handles a request for a list of entities, calling a method asynchronously with an entity parameter, 
    /// and returns appropriate responses based on the method result and modal state.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with the result of the operation.</returns>
    public async Task<StandardApiResponse> ReturnListMethods<TReturn, TAccept>(Func<TAccept, Task<StandardServiceResponse>> method, bool modalState, TAccept entity, MessageType successMsgType, MessageType notFoundMsgType, MessageType badRequestMsgType)
    {
        StandardApiResponse apiResponse = new();
        try
        {
            if (modalState)
            {
                StandardServiceResponse standardServiceResponse = await method.Invoke(entity);

                var configValueString = _configuration["InnerLayoutOrientation"];
                int configValue = int.TryParse(configValueString, out var val) ? val : 0;

                apiResponse.InnerLayoutOrientation = standardServiceResponse.InnerLayoutOrientation > 0
                    ? standardServiceResponse.InnerLayoutOrientation
                    : configValue;

                if (standardServiceResponse.IsValide)
                {
                    apiResponse = HandleListSuccess<TReturn>(standardServiceResponse.InnerLayoutOrientation, (IList<TReturn>)standardServiceResponse.Data, successMsgType);
                }
                else
                {
                    apiResponse = HandleListNotFound(apiResponse.InnerLayoutOrientation, notFoundMsgType);
                }
            }
            else
            {
                apiResponse = HandleListBadRequest(apiResponse.InnerLayoutOrientation, badRequestMsgType);
            }
        }
        catch (SqlException ex)
        {
            apiResponse = HandleListInternalServerError(apiResponse.InnerLayoutOrientation, ex);
        }
        catch (Exception ex)
        {
            apiResponse = HandleListInternalServerError(apiResponse.InnerLayoutOrientation, ex);
        }
        return apiResponse;
    }

    /// <summary>
    /// Handles a request for a single entity, calling a method asynchronously, 
    /// and returns appropriate responses based on the method result and modal state.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with the result of the operation.</returns>
    public async Task<StandardApiResponse> ReturnObjectMethodsWithoutEntity<TReturn>(Func<Task<StandardServiceResponse>> method, bool modalState, MessageType successMsgType, MessageType notFoundMsgType, MessageType badRequestMsgType)
    {
        StandardApiResponse apiResponse = new();
        try
        {
            if (modalState)
            {
                StandardServiceResponse standardServiceResponse = await method.Invoke();

                var configValueString = _configuration["InnerLayoutOrientation"];
                int configValue = int.TryParse(configValueString, out var val) ? val : 0;

                apiResponse.InnerLayoutOrientation = standardServiceResponse.InnerLayoutOrientation > 0
                    ? standardServiceResponse.InnerLayoutOrientation
                    : configValue;

                if (standardServiceResponse.IsValide)
                {
                    apiResponse = HandleObjectSuccess<TReturn>(standardServiceResponse.InnerLayoutOrientation, (TReturn)standardServiceResponse.Data, successMsgType);
                }
                else
                {
                    apiResponse = HandleObjectNotFound(apiResponse.InnerLayoutOrientation, notFoundMsgType);
                }
            }
            else
            {
                apiResponse = HandleObjectBadRequest(apiResponse.InnerLayoutOrientation, badRequestMsgType);
            }
        }
        catch (SqlException ex)
        {
            apiResponse = HandleObjectInternalServerError(apiResponse.InnerLayoutOrientation, ex);
        }
        catch (Exception ex)
        {
            apiResponse = HandleObjectInternalServerError(apiResponse.InnerLayoutOrientation, ex);
        }
        return apiResponse;
    }

    /// <summary>
    /// Handles a request for a single entity, calling a method asynchronously with an entity parameter, 
    /// and returns appropriate responses based on the method result and modal state.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with the result of the operation.</returns>
    public async Task<StandardApiResponse> ReturnObjectMethods<TReturn, TAccept>(Func<TAccept, Task<StandardServiceResponse>> method, bool modalState, TAccept entity, MessageType successMsgType, MessageType notFoundMsgType, MessageType badRequestMsgType)
    {
        StandardApiResponse apiResponse = new();
        try
        {
            if (modalState)
            {
                StandardServiceResponse standardServiceResponse = await method.Invoke(entity);

                var configValueString = _configuration["InnerLayoutOrientation"];
                int configValue = int.TryParse(configValueString, out var val) ? val : 0;

                apiResponse.InnerLayoutOrientation = standardServiceResponse.InnerLayoutOrientation > 0
                    ? standardServiceResponse.InnerLayoutOrientation
                    : configValue;

                if (standardServiceResponse.IsValide)
                {
                    apiResponse = HandleObjectSuccess<TReturn>(
                        standardServiceResponse.InnerLayoutOrientation,
                        (TReturn)standardServiceResponse.Data!,
                        successMsgType);
                }
                else
                {
                    apiResponse = HandleObjectNotFound(apiResponse.InnerLayoutOrientation, notFoundMsgType);

                }
            }
            else
            {
                apiResponse = HandleObjectBadRequest(apiResponse.InnerLayoutOrientation, badRequestMsgType);
            }
        }
        catch (SqlException ex)
        {
            apiResponse = HandleObjectInternalServerError(apiResponse.InnerLayoutOrientation, ex);
        }
        catch (Exception ex)
        {
            apiResponse = HandleObjectInternalServerError(apiResponse.InnerLayoutOrientation, ex);
        }
        return apiResponse;
    }

    /// <summary>
    /// Handles a request for a null list of entities, calling a method asynchronously with an entity parameter, 
    /// and returns appropriate responses based on the method result and modal state.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with the result of the operation.</returns>
    public async Task<StandardApiResponse> ReturnNullListMethods<TAccept>(Func<TAccept, Task<StandardServiceResponse>> method, bool modalState, TAccept entity, MessageType successMsgType, MessageType notFoundMsgType, MessageType badRequestMsgType)
    {
        StandardApiResponse apiResponse = new();
        try
        {
            if (modalState)
            {
                StandardServiceResponse standardServiceResponse = await method.Invoke(entity);

                var configValueString = _configuration["InnerLayoutOrientation"];
                int configValue = int.TryParse(configValueString, out var val) ? val : 0;

                apiResponse.InnerLayoutOrientation = standardServiceResponse.InnerLayoutOrientation > 0
                    ? standardServiceResponse.InnerLayoutOrientation
                    : configValue;

                if (standardServiceResponse.IsValide)
                {
                    apiResponse = HandleListSuccess<NullColumns>(standardServiceResponse.InnerLayoutOrientation, [], successMsgType);
                }
                else
                {
                    apiResponse = HandleListNotFound(apiResponse.InnerLayoutOrientation, notFoundMsgType);
                }
            }
            else
            {
                apiResponse = HandleListBadRequest(apiResponse.InnerLayoutOrientation, badRequestMsgType);
            }
        }
        catch (SqlException ex)
        {
            apiResponse = HandleListInternalServerError(apiResponse.InnerLayoutOrientation, ex);
        }
        catch (Exception ex)
        {
            apiResponse = HandleListInternalServerError(apiResponse.InnerLayoutOrientation, ex);
        }
        return apiResponse;
    }

    /// <summary>
    /// Handles a request for a null object, calling a method asynchronously with an entity parameter, 
    /// and returns appropriate responses based on the method result and modal state.
    /// </summary>
    /// <returns>A <see cref="StandardApiResponse"/> with the result of the operation.</returns>
    public async Task<StandardApiResponse> ReturnNullObjectMethods<TAccept>(Func<TAccept, Task<StandardServiceResponse>> method, bool modalState, TAccept entity, MessageType successMsgType, MessageType notFoundMsgType, MessageType badRequestMsgType)
    {
        StandardApiResponse apiResponse = new();
        try
        {
            if (modalState)
            {
                StandardServiceResponse standardServiceResponse = await method.Invoke(entity);

                var configValueString = _configuration["InnerLayoutOrientation"];
                int configValue = int.TryParse(configValueString, out var val) ? val : 0;

                apiResponse.InnerLayoutOrientation = standardServiceResponse.InnerLayoutOrientation > 0
                    ? standardServiceResponse.InnerLayoutOrientation
                    : configValue;

                if (standardServiceResponse.IsValide)
                {
                    apiResponse = HandleObjectSuccess<NullColumns>(standardServiceResponse.InnerLayoutOrientation, new(), successMsgType);
                }
                else
                {
                    apiResponse = HandleObjectNotFound(apiResponse.InnerLayoutOrientation, notFoundMsgType);
                }
            }
            else
            {
                apiResponse = HandleObjectBadRequest(apiResponse.InnerLayoutOrientation, badRequestMsgType);
            }
        }
        catch (SqlException ex)
        {
            apiResponse = HandleObjectInternalServerError(apiResponse.InnerLayoutOrientation, ex);
        }
        catch (Exception ex)
        {
            apiResponse = HandleObjectInternalServerError(apiResponse.InnerLayoutOrientation, ex);
        }
        return apiResponse;
    }
}
