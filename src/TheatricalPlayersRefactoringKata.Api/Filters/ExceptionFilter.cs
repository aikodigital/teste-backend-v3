using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Exception;
using TheatricalPlayersRefactoringKata.Exception.ExceptionsBase;

namespace TheatricalPlayersRefactoringKata.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        // Implements function of interface
        public void OnException(ExceptionContext context)
        {
            // If error of exception configured
            if (context.Exception is TheatricalPlayersRefactoringKataException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnknownError(context);
            }
        }

        private void HandleProjectException(ExceptionContext context)
        {
            var theatricalPlayersRefactoringKataException = context.Exception as TheatricalPlayersRefactoringKataException;
            var errorResponse = new ResponseErrorJson(theatricalPlayersRefactoringKataException!.GetErrors());

            context.HttpContext.Response.StatusCode = theatricalPlayersRefactoringKataException.StatusCode;
            context.Result = new ObjectResult(errorResponse);
        }

        private void ThrowUnknownError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
