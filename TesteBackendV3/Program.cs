using Aplication.DTO;
using Aplication.Services.Formatters;
using Aplication.Services.Interfaces;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPlayService, Aplication.Services.PlayService>();
builder.Services.AddScoped<IStatementService, Aplication.Services.StatementService>();

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

app.MapGet("/statement", (IStatementService statementService) =>
{
    var impressao = statementService.Print(statementService.ObterInvoiceBigCo2(), new TextoInvoiceFormater());
    return Results.Ok(impressao);
})
    .WithName("GetStatement").WithTags("Statement");


app.Run();
