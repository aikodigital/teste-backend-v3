using System.Reflection;
using TheatricalPlayersRefactoringKata.Application.UseCases;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Queues;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;
using TheatricalPlayersRefactoringKata.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton(provider => new Dictionary<string, Play>
{
    { "hamlet", new Play("Hamlet", 4024, "tragedy") },
    { "as-like", new Play("As You Like It", 2670, "comedy") },
    { "othello", new Play("Othello", 3560, "tragedy") },
    { "henry-v", new Play("Henry V", 3227, "history") },
    { "richard-iii", new Play("Richard III", 3718, "history") }
});

builder.Services.AddSingleton<IPlayRepository, PlayRepository>();
builder.Services.AddSingleton<IStatementQueue, StatementQueue>();

builder.Services.AddTransient<GenerateStatementUseCase>();
builder.Services.AddTransient<EnqueueStatementUseCase>();

builder.Services.AddTransient<XmlStatementPrinter>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
