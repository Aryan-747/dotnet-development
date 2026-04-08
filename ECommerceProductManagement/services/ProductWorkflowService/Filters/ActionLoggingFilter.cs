using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ProductWorkflowService.Filters;

public class ActionLoggingFilter : IAsyncActionFilter
{
    private readonly ILogger<ActionLoggingFilter> _logger;

    public ActionLoggingFilter(ILogger<ActionLoggingFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
        var controller = descriptor?.ControllerName ?? "UnknownController";
        var action = descriptor?.ActionName ?? "UnknownAction";
        var request = context.HttpContext.Request;
        var user = context.HttpContext.User?.Identity?.IsAuthenticated == true
            ? context.HttpContext.User.Identity?.Name ?? "authenticated-user"
            : "anonymous";
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation(
            "Action started: {RequestMethod} {RequestPath} -> {Controller}.{Action} by {User}",
            request.Method,
            request.Path,
            controller,
            action,
            user);

        var executedContext = await next();

        stopwatch.Stop();

        if (executedContext.Exception is not null && !executedContext.ExceptionHandled)
        {
            _logger.LogError(
                executedContext.Exception,
                "Action failed: {RequestMethod} {RequestPath} -> {Controller}.{Action} in {ElapsedMs} ms",
                request.Method,
                request.Path,
                controller,
                action,
                stopwatch.ElapsedMilliseconds);

            return;
        }

        _logger.LogInformation(
            "Action completed: {RequestMethod} {RequestPath} -> {Controller}.{Action} responded {StatusCode} in {ElapsedMs} ms",
            request.Method,
            request.Path,
            controller,
            action,
            executedContext.HttpContext.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}
