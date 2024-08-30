using AutoMapper;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models.Dtos;

namespace TheatricalPlayersRefactoringKata.Endpoints;

internal static class StatementEndpoint
{
    public static RouteGroupBuilder MapStatement(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/statement");

        group.WithTags("Statement");

        group.MapGet("/", async (IStatementService service, IMapper mapper) =>
        {
            var result = await service.GetAllStatementsAsync();

            return TypedResults.Ok(mapper.Map<List<StatementDto>>(result));
        });

        return group;
    }
}
