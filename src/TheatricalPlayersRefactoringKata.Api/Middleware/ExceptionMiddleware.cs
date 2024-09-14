using System.Net;
using System.Text.Json;
using TheatricalPlayersRefactoringKata.Application.Response;

namespace TheatricalPlayersRefactoringKata.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            RespostaPadrao response;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                response = new RespostaPadrao(false, $"Exception: Mensagem: {ex.Message}; InnerException: {ex?.InnerException?.Message}; StackTrace: {ex?.StackTrace}");
            else
                response = new RespostaPadrao(false, "Ocorreu um erro no processamento da sua requisição. Por favor tente novamente mais tarde.");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
