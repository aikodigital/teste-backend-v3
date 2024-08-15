using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Exception.ExceptionBase;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Exception;

namespace TheatricalPlayersAPI.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is TheatricalPlayersRefactoringKataException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnkowError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidation)
        {
            var ex = (ErrorOnValidation)context.Exception;

            var errorResponse = new ResponseError(ex.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
        else
        {
            var errorResponse = new ResponseError(context.Exception.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }

    private void ThrowUnkowError(ExceptionContext context)
    {
        var errorResponse = new ResponseError(ResourceErrorMessages.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}