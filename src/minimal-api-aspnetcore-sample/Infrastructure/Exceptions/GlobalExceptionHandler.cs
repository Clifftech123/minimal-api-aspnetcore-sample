using Microsoft.AspNetCore.Diagnostics;
using minimal_api_aspnetcore_sample.Endpoints.Contracts;
using System.Net;

namespace minimal_api_aspnetcore_sample.Infrastructure.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError("An error occurred while processing your request");

        var errorResponse = new ErrorResponse()
        {
            Message = exception.Message
        };

        // Determine the status code and title based on the type of exception.
        switch (exception)
        {
            case BadHttpRequestException:
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Title = exception.GetType().Name;
                break;

            case RecipeNotFoundException:
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Title = "Recipe Not Found";
                break;

            case InstructionDoesNotExistException:
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Title = "Instruction Does Not Exist";
                break;

            case IngredientDoesNotExistException:
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Title = "Ingredient Does Not Exist";
                break;

            default:
                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Title = "Internal Server Error";
                break;
        }

        httpContext.Response.StatusCode = errorResponse.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

        // Return true to indicate that the exception was handled.
        return true;
    }
}
