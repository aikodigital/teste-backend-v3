using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Endpoints;

internal static class ExtractEndpoint
{
    public static RouteGroupBuilder MapExtract(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/extract");

        group.WithTags("Extract");

        group.MapPost("/", async (ApplicationDbContext db) =>
        {
            await Task.CompletedTask;
            return TypedResults.Content("OK", "text/plain");
        });

        return group;
    }
}

