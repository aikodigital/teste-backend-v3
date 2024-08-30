using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Endpoints;

internal static class ExtractEndpoint
{
    public static RouteGroupBuilder MapExtract(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/extract");

        group.WithTags("Extract");

        group.MapPost("/", (StatementProcessingService service) =>
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
        });

        return group;
    }
}

