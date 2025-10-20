using Domain.Common;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Net.Http.Headers;
using Serilog;
using System.Security.Claims;
using System.Text;

namespace AppvilaAPI.Helpers.Middleware;

public class CustomMiddleware(IUserContext userContext) : IMiddleware
{
    private readonly IUserContext _userContext = userContext;

    /// <summary>
    /// Intercepts each HTTP request, logs request and response in one Serilog record,
    /// and sets language preference for the current user context.
    /// </summary>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string requestBody = await ReadRequestBodyAsync(context);

        // Keep original response stream
        var originalBodyStream = context.Response.Body;
        await using var responseBodyMemoryStream = new MemoryStream();
        context.Response.Body = responseBodyMemoryStream;

        // Create base logger for this request (shared between request + response)
        var logger = CreateRequestLogger(context, requestBody);

        try
        {
            // Set user language preference
            SetLanguagePreference(context);

            // Continue pipeline
            await next(context);

            // Capture the response body (after controller executed)
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            // Add response body into same log entry
            logger
                .ForContext("ResponseBody", responseBody)
                .Information("Request completed successfully");
        }
        catch (Exception ex)
        {
            // Log exception and update response
            logger.Error(ex, "Unhandled exception occurred during request processing");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
        finally
        {
            // Copy response back to original stream
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBodyMemoryStream.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }
    }

    /// <summary>
    /// Reads the request body safely while allowing further reading downstream.
    /// </summary>
    private static async Task<string> ReadRequestBodyAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
        string body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;
        return body;
    }

    /// <summary>
    /// Creates a Serilog logger prefilled with request context properties.
    /// This logger is used for both request and response logging (same record).
    /// </summary>
    private static Serilog.ILogger CreateRequestLogger(HttpContext context, string requestBody)
    {
        string userName = "Anonymous";

        if (context.User.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
        {
            userName = identity.FindFirst(ClaimTypes.Name)?.Value
                       ?? identity.FindFirst(ClaimTypes.Email)?.Value
                       ?? "Unknown";
        }

        return Log
            .ForContext("UserName", userName)
            .ForContext("RemoteIPAddress", context.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "Unknown")
            .ForContext("BrowserType", GetHeaderValue(context, HeaderNames.UserAgent))
            .ForContext("RequestId", context.Request.Headers["X-Request-Id"].FirstOrDefault() ?? context.TraceIdentifier)
            .ForContext("RequestUrl", context.Request.GetDisplayUrl())
            .ForContext("RequestBody", requestBody)
            .ForContext("SystemName", "Appvila");
    }

    /// <summary>
    /// Retrieves a specific request header safely.
    /// </summary>
    private static string GetHeaderValue(HttpContext context, string headerName)
    {
        return context.Request.Headers.TryGetValue(headerName, out var value)
            ? value.ToString()
            : string.Empty;
    }

    /// <summary>
    /// Sets language preference based on the "Accept-Language" header.
    /// </summary>
    private void SetLanguagePreference(HttpContext context)
    {
        string langId = GetHeaderValue(context, "Accept-Language");
        _userContext.LangId = Methods.ValidateLanguage(langId).ToString();
    }
}
