using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Filters;

public sealed class OperationCancelledExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger _logger;

    public OperationCancelledExceptionFilter(ILoggerFactory loggerFactory) {
        _logger = loggerFactory.CreateLogger<OperationCancelledExceptionFilter>();
    }

    public override void OnException(ExceptionContext context) {
        if (context.Exception is OperationCanceledException) {
            _logger.LogWarning("Request was cancelled");
            context.ExceptionHandled = true;
            context.Result = new StatusCodeResult(400);
        }
    }
}
