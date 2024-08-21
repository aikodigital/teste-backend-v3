using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Data.Common;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddControllers();

// Configuração Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Aiko Theatre API",
        Version = "v1",
        Description = "Atividade de refatoração de código proposta pela Aiko.",
    });
});


builder.Services.AddScoped<TragedyCalculator>();
builder.Services.AddScoped<ComedyCalculator>();
builder.Services.AddScoped<HistoryCalculator>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aiko Theatre API");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();