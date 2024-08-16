using System.Net;

namespace TheatricalPlayersAPI.Models;

public class ResponseModel<T>
{
    public T? data { get; set; }
    public string message { get; set; }
    public HttpStatusCode statusCode { get; set; }
}