using Aplication.DTO;
using Aplication.Services.Interfaces;

namespace TesteBackendV3
{
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
}
