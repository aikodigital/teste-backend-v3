using System.Net;

namespace TheatricalPlayersRefactoringKata.Application.DTOs
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public HttpStatusCode Status { get; set; }
    }
}
