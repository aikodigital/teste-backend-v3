using Aplication.DTO;
using Aplication.Services.Formatters;
using Aplication.Services.Interfaces;
using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPlayService, Aplication.Services.PlayService>();
builder.Services.AddScoped<IStatementService, Aplication.Services.StatementService>();

builder.Services.AddDbContext<TesteBackendV3DbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/plays", (IPlayService playService, IMapper mapper) =>
{
    var playsDtos = playService.GetPlays();
    return Results.Ok(playsDtos);
}).WithName("GetPlays").WithTags("Plays");

app.MapGet("/statementText", (IStatementService statementService) =>
{
    var impressao = statementService.Print(statementService.ObterInvoiceBigCo2(), new TextoInvoiceFormater());
    return Results.Ok(impressao);
})
.WithName("GetStatementText").WithTags("Statement");


app.MapGet("/statementXml", (IStatementService statementService) =>
{
    var impressao = statementService.Print(statementService.ObterInvoiceBigCo2(), new XmlInvoiceFormatter());
    return Results.Text(impressao, "application/xml");
})
.WithName("GetStatementXml").WithTags("Statement");

app.MapPost("/invoice", async (InvoiceDto invoiceDto, IStatementService statementService) =>
{
    try
    {
        await statementService.InsertInvoice(invoiceDto);
        return Results.Ok(new { Message = "Fatura inserida com sucesso" });
    }
    catch (Exception ex) { return Results.Problem(ex.Message.ToString()); }
}).WithName("PostInvoice").WithTags("Invoices");

app.MapGet("/statementSaved", async (IStatementService statementService) =>
{
    var invoices = await statementService.GetInvoices();
    return Results.Ok(invoices);
}).WithName("GetInvoices").WithTags("Invoices");

app.Run();
