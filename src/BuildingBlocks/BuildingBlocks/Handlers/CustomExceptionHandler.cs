using BuildingBlocks.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Handlers
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "An unhandled exception has occurred: {Message}. Time: {Time}", exception.Message, DateTime.UtcNow);

            (string Details, string Title, int StatusCode) details = exception switch
            {
                NotFoundException => (exception.Message, "Not Found", StatusCodes.Status404NotFound),
                BedRequestException => (exception.Message, "Bad Request", StatusCodes.Status400BadRequest),
                InternalServerException => (exception.Message, "Internal Server Error", StatusCodes.Status500InternalServerError),
                ValidationException => (exception.Message, "Validation Error", StatusCodes.Status400BadRequest),
                _ => ("An error occurred while processing your request.", "An error occurred", StatusCodes.Status500InternalServerError)
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Status = details.StatusCode,
                Detail = details.Details
            };

            httpContext.Response.StatusCode = details.StatusCode;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
