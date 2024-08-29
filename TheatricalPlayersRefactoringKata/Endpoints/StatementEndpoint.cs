using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Endpoints;

internal static class StatementEndpoint
{
    public static RouteGroupBuilder MapStatement(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/statement");

        group.WithTags("Statement");

        group.MapGet("/{id}", async Task<Results<Ok<Statement>, NotFound>> (ApplicationDbContext db, int id) =>
        {
            return await db.Statements.FindAsync(id) switch
            {
                Statement stmt => TypedResults.Ok(stmt),
                null => TypedResults.NotFound(),
            };
        });

        return group;
    }
}
