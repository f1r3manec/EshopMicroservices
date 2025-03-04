using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message : { exceptionMessage}, Time of occurrence {time}", exception.Message, DateTime.UtcNow);
         (string Detail, string Tittle, int StatusCode) details = exception switch
         {
             InternalServerExecption=> (exception.Message, GetType().Name, StatusCodes.Status500InternalServerError),
             ValidationException  => (exception.Message, GetType().Name, StatusCodes.Status400BadRequest),
             BadRequestExecption=> (exception.Message, GetType().Name, StatusCodes.Status400BadRequest),
             NotFoundExceptions notFoundExceptions => (exception.Message, GetType().Name, StatusCodes.Status404NotFound),
             _ => (exception.Message, GetType().Name, StatusCodes.Status500InternalServerError)
         };
            var problemDetails = new ProblemDetails
            {
                Title = details.Tittle,
                Status = details.StatusCode,
                Detail = details.Detail,
                Instance = context.Request.Path

            };
            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("Validation Errors", validationException.Errors);
            }
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }
    }
}
