using Aplication.Services.Formatters;
using Aplication.Services.Interfaces;

namespace TesteBackendV3
{
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
