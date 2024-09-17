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
builder.Services.AddSingleton<StatementProcessingService>();
builder.Services.AddDbContext<TheaterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("TheatricalPlayersRefactoringKata.API")));
WebApplication app = builder.Build();
DatabaseManagementService.MigrationInitialisation(app);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

#region Plays Endpoint
app.MapGet("/api/plays", async (TheaterDbContext dbContext) =>
{
    List<Play> plays = await dbContext.Plays.ToListAsync();
    return Results.Ok(plays);
});

app.MapGet("/api/plays/{id}", async (string id, TheaterDbContext dbContext) =>
{
    Play? play = await dbContext.Plays.FindAsync(id);
    if (play == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(play);
});

app.MapPost("/api/plays", async (Play play, TheaterDbContext dbContext) =>
{
    dbContext.Plays.Add(play);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/api/plays/{play.PlayId}", play);
});
#endregion Plays Endpoint

#region Performance Endpoint
app.MapGet("/api/performances", async (TheaterDbContext dbContext) =>
{
    List<Performance> performances = await dbContext.Performances.ToListAsync();
    return Results.Ok(performances);
});

app.MapGet("/api/performances/{playId}", async (string playId, TheaterDbContext dbContext) =>
{
    Performance? performance = await dbContext.Performances.FindAsync(playId);
    if (performance == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(performance);
});

app.MapPost("/api/performances", async (Performance performance, TheaterDbContext dbContext) =>
{
    dbContext.Performances.Add(performance);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/api/performances/{performance.PlayId}", performance);
});
#endregion Performance Endpoint
await app.RunAsync();
