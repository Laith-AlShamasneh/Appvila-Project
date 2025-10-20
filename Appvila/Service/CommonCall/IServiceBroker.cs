using Domain.Common;

namespace Service.CommonCall;

/// <summary>
/// Defines a set of asynchronous methods for handling API responses, including operations for lists and objects.
/// These methods allow returning standard API responses based on the results of various service methods, with support for modal state and different message types.
/// </summary>
public interface IServiceBroker<TController>
{
    public Task<StandardApiResponse> ReturnListMethodsWithoutEntity<TReturn>(Func<Task<StandardServiceResponse>> method, bool modalState, MessageType successMsgType, MessageType notFoundMsgType, MessageType badRequestMsgType);

    public Task<StandardApiResponse> ReturnListMethods<TReturn, TAccept>(Func<TAccept, Task<StandardServiceResponse>> method, bool modalState, TAccept entity, MessageType successMsgType, MessageType notFoundMsgType, MessageType badRequestMsgType);

    public Task<StandardApiResponse> ReturnObjectMethodsWithoutEntity<TReturn>(Func<Task<StandardServiceResponse>> method, bool modalState, MessageType successMsgType, MessageType notFoundMsgType, MessageType badRequestMsgType);

    public Task<StandardApiResponse> ReturnObjectMethods<TReturn, TAccept>(Func<TAccept, Task<StandardServiceResponse>> method, bool modalState, TAccept entity, MessageType successMsgType, MessageType notFoundMsgType, MessageType badRequestMsgType);

    public Task<StandardApiResponse> ReturnNullListMethods<TAccept>(Func<TAccept, Task<StandardServiceResponse>> method, bool modalState, TAccept entity, MessageType successMsgType, MessageType notFoundMsgType, MessageType badRequestMsgType);

    public Task<StandardApiResponse> ReturnNullObjectMethods<TAccept>(Func<TAccept, Task<StandardServiceResponse>> method, bool modalState, TAccept entity, MessageType successMsgType, MessageType notFoundMsgType, MessageType badRequestMsgType);
}
