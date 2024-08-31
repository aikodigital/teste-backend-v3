using AutoMapper;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Application.Models.Dtos;
using TheatricalPlayersRefactoringKata.Application.Services;

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
        })
        .WithSummary("Retorna os extratos cadastrando durante a geração dos xml's na rota: /statement/queue");

        group.MapPost("/queue", (StatementProcessingService service) =>
        {
            // TODO: PODERIA VIR POR PARAMETRO
            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("henry-v", 20)
                }
            );

            service.EnqueueInvoice(invoice);

            return TypedResults.Content("Invoice enqueued successfully.", "text/plain");
        })
        .WithSummary("Gera os xml's assincronicamente na pasta configurada no appsettings.json");

        return group;
    }
}
