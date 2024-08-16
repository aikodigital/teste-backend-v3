
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Exception.ExceptionBase;
using TheatricalPlayersRefactoringKata.Exception;

namespace TheatricalPlayersAPI.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
            HandleException(context);
      
    }

    private void HandleException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidation)
        {
            ErrorOnValidation ex = (ErrorOnValidation)context.Exception;
            var errorResponse = new ResponseError(ex.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new ObjectResult(errorResponse);
        }
        else if (context.Exception is NotFoundException ex)
        {
            var errorResponse = new ResponseError(ex.Message);
            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Result = new NotFoundObjectResult(errorResponse);
        }
        else
        {
            var errorResponse = new ResponseError(context.Exception.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            context.Result = new ObjectResult(errorResponse);
        }
    }
}