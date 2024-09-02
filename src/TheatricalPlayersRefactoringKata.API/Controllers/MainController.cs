using Microsoft.AspNetCore.Mvc;

namespace TheatricalPlayersRefactoringKata.API.Controllers;

public abstract class MainController : ControllerBase
{
    /// <summary>
    /// Retrieves the error messages from the model state.
    /// </summary>
    /// <returns>An enumerable collection of error messages from the model state.</returns>
    protected IEnumerable<string> GetModelStateErrors()
    {
        return ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage);
    }

    /// <summary>
    /// Retrieves the requested API version from the HTTP context.
    /// </summary>
    /// <returns>The requested API version as a string, or null if no version is specified.</returns>
    protected string? GetApiVersion()
    {
        return HttpContext.GetRequestedApiVersion()?.ToString();
    }
}