using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Context;
using TheatricalPlayersRefactoringKata.DTO;
using TheatricalPlayersRefactoringKata.Formatter;
using TheatricalPlayersRefactoringKata.Interface;
using TheatricalPlayersRefactoringKata.Model;
using TheatricalPlayersRefactoringKata.Service;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurando o canal e serviços necessários
builder.Services.AddSingleton<IStatementFormatter, XmlStatementFormatter>();
//builder.Services.AddSingleton<Channel<Invoice>>(Channel.CreateUnbounded<Invoice>());
builder.Services.AddSingleton<StatementProcessingService>();
builder.Services.AddDbContext<TheaterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Minimal API: Rota para processar fatura
app.MapPost("/api/invoice/process", async (Invoice invoice, TheaterDbContext dbContext, StatementProcessingService processingService) =>
{
    dbContext.Invoices.Add(invoice);
    await dbContext.SaveChangesAsync();

    string statement = await processingService.ProcessInvoiceAsync(invoice);

    return Results.Ok(statement);
});

// Minimal API: Rota para verificar o status de processamento
app.MapGet("/api/invoice/status/{customer}", (string customer) =>
{
    string filePath = Path.Combine("Output", $"{customer}_statement.xml");
    return File.Exists(filePath)
        ? Results.Ok(new { Status = "Completed", FilePath = filePath })
        : Results.NotFound("Statement not found or still processing.");
});
app.MapPost("/api/invoice", async (InvoiceDto invoiceDto, TheaterDbContext context) =>
{
    var invoice = new Invoice
    {
        Customer = invoiceDto.Customer,
        Performances = invoiceDto.Performances.Select(perf => new Performance
        {
            PlayId = perf.PlayId,
            Audience = perf.Audience,
        }).ToList()
    };

    var plays = invoiceDto.Plays.Select(p => new Play
    {
        Name = p.Name,
        Type = p.Type,
        Lines = p.Lines
    }).ToList();

    foreach (Play? play in plays)
    {
        if (!context.Plays.Any(dbPlay => dbPlay.Name == play.Name))
        {
            context.Plays.Add(play);
        }
    }
    context.Invoices.Add(invoice);
    await context.SaveChangesAsync();

    return Results.Ok("Invoice saved successfully!");
});
await app.RunAsync();
