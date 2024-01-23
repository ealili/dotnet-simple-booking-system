using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBookingSystem.Infrastructure;

internal sealed class FluentValidationExceptionHandler : IExceptionHandler
{
    private readonly ILogger<FluentValidationExceptionHandler> _logger;

    public FluentValidationExceptionHandler(ILogger<FluentValidationExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        _logger.LogError(
            validationException, "FluentValidationException occurred: {Message}", validationException.Message);

        var errors = validationException.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status422UnprocessableEntity,
            Title = "Validation Failed",
            Detail = "One or more validation errors occurred.",
            Extensions = { ["errors"] = errors }
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}