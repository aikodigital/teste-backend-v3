using Aplication.DTO;
using Aplication.Services.Formatters;
using Aplication.Services.Interfaces;
using AutoMapper;

namespace TesteBackendV3
{
    public static class PerformanceEndpoints
    {
        
        

    }


    public static class PlayEndpoints
    {
        public static void GetPlays(WebApplication app)
        {
            app.MapGet("/plays", (IPlayService playService, IMapper mapper) =>
            {
                var playsDtos = playService.GetPlays();
                return Results.Ok(playsDtos);
            }).WithName("GetPlays").WithTags("Plays");
        }
    }

    public static class InvoiceEndpoints
    {
        public static void InvoicePost(WebApplication app)
        {
            app.MapPost("/invoice", async (InvoiceDto invoiceDto, IStatementService statementService) =>
            {
                try
                {
                    await statementService.InsertInvoice(invoiceDto);
                    return Results.Ok(new { Message = "Fatura inserida com sucesso" });
                }
                catch (Exception ex) { return Results.Problem(ex.Message.ToString()); }
            }).WithName("PostInvoice").WithTags("Invoices");
        }
    }

    public static class StatementEndpoints
    {
        public static void StatementSaved(WebApplication app)
        {
            app.MapGet("/statementSaved", async (IStatementService statementService) =>
            {
                var invoices = await statementService.GetInvoices();
                return Results.Ok(invoices);
            }).WithName("GetInvoices").WithTags("Invoices");
        }
        public static void StatementXml(WebApplication app)
        {
            app.MapGet("/statementXml", (IStatementService statementService) =>
            {
                var impressao = statementService.Print(statementService.ObterInvoiceBigCo2(), new XmlInvoiceFormatter());
                return Results.Text(impressao, "application/xml");
            })
            .WithName("GetStatementXml").WithTags("Statement");
        }
        public static void StatementText(WebApplication app)
        {
            app.MapGet("/statementText", (IStatementService statementService) =>
            {
                var impressao = statementService.Print(statementService.ObterInvoiceBigCo2(),
                    new TextoInvoiceFormater());
                return Results.Ok(impressao);
            })
            .WithName("GetStatementText").WithTags("Statement");
        }
    }
}
